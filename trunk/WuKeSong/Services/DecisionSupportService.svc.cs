using System;
using WuKeSong.Interfaces;
using YuQuan.Models;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Workflow.Runtime;
using YuQuan.Helpers;
using System.Diagnostics;
using RuleSetService = WuKeSong.Services.RuleSetService;

namespace WuKeSong.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DecisionSupportService" in code, svc and config file together.
    public class DecisionSupportService: IDecisionSupportService
    {
        private KBEntities db = new KBEntities ();
        private IFactService factService = new FactService();
        static AutoResetEvent waitHandle = new AutoResetEvent(false);
        private string currentUser;
        public string CurrentUser
        {
            get 
            {
                if (string.IsNullOrEmpty(currentUser))
                {
                    if (System.Web.Security.Membership.GetUser() != null)
                        return System.Web.Security.Membership.GetUser().UserName;
                    else
                        return "";
                }
                else
                {
                    return currentUser;
                }
            }
            set
            {
                currentUser = value;
            }
        }

        /// <summary>
        /// Run Rule Engine to detect clinical problems.        
        /// </summary>
        /// <param name="context">collection of fields for reasoning</param>
        /// <returns>Detected clinical problems</returns>
        public IEnumerable<string> Reason(IEnumerable<ClinicalProblemDefinition> candidateProblems, Context context)
        {
            //
            // execute ruleset of each problem
            TriggerRuleWorkflow.ExchangeContext = context.Clone() as Context;
            candidateProblems.ToList().ForEach(problem =>
            {
                if (problem.TriggerRule == null)
                    return;

                TriggerRuleWorkflow.RuleSetName = problem.TriggerRule.Name;

                var workflowRuntime = new WorkflowRuntime();
                workflowRuntime.WorkflowCompleted += (sender, e) => { waitHandle.Set(); };
                workflowRuntime.WorkflowTerminated += (sender, e) => { waitHandle.Set(); };
                workflowRuntime.AddService(new RuleSetService());
                var instance = workflowRuntime.CreateWorkflow(typeof(TriggerRuleWorkflow));
                instance.Start();

                waitHandle.WaitOne();
                workflowRuntime.StopRuntime();
            });// Each workflow engine instance takes 2ms.

            //
            // Get the triggered problems
            var triggeredProblems = TriggerRuleWorkflow.Problems.ToList(); // ToList() is necessary, because soon we will clear Problems.
            // [TODO] TriggerRuleWorkflow.Problems is static, what if multi thread call?  
            TriggerRuleWorkflow.Problems.Clear();

            // do something according to returned problems...    
            // Fire event to other modules...
            return triggeredProblems.Distinct().ToList();
        }

        /// <summary>
        /// Preprocess abnormal facts to get candidate problems. Other normal facts were updated to Encounter Fact Cache/Profile.
        /// </summary>
        /// <param name="triggers">abnormal facts</param>
        /// <returns>candidate problems</returns>
        public IEnumerable<ClinicalProblemDefinition> GetCandidateProblems(IEnumerable<Fact> triggers)
        {
            if (triggers == null || triggers.Count() <= 0)
                return null;

            //
            // iterate through context to determine valid problems
            var contextItemIds = triggers.Select(x => x.ContextItemDefinition.Id).ToList();

            // 
            // iterate through all problems to see which ones contains the context items. This procedure takes 1.2s (4419 records).
            var candidateProblems = new List<ClinicalProblemDefinition>();

            if(contextItemIds.Count()<=0)
                return candidateProblems;

            var problemIds = db.ClinicalProblemDefinition_ContextItemDefinition_AssociationSet.Where(x => contextItemIds.Contains(x.ContextItemDefinition_Id)).Select(x => x.ClinicalProblemDefinition_Id).Distinct().ToList();

            /*
            var contextItems = triggers.ToList().Select(x => x.ContextItemDefinition);
            foreach (var problem in db.ClinicalProblemDefinition)
            {
                if (problem.ContextItemDefinition != null && 
                    problem.TriggerRule != null && // if no rule is defined, skip.
                    problem.ContextItemDefinition.Intersect(contextItems, new PropertyComparer<ContextItemDefinition>("Id")).Count() > 0)
                {
                    candidateProblems.Add(problem);
                }
            };
            */

            problemIds.ForEach(x =>
            {
                var problem = db.ClinicalProblemDefinition.Find(x);
                if (problem != null)
                    candidateProblems.Add(problem);
            });

            return candidateProblems;

            // NOTE: Directly read association table may outperform the DBContext way.
        }

        /// <summary>
        /// Construct reasoning context, meanwhile filter candidate problems further by checking whether required facts are available.
        /// </summary>
        /// <param name="encounter">patient encounter, where data come from its profile</param>
        /// <param name="candidateProblems"></param>
        /// <param name="filteredProblems"></param>
        /// <param name="reasoningContext"></param>
        public void ConstructReasoningContext(Encounter encounter, IEnumerable<ClinicalProblemDefinition> candidateProblems, out IEnumerable<ClinicalProblemDefinition> filteredProblems, out Context reasoningContext)
        {
            if (encounter == null ||
                encounter.Fact.Count() <= 0)
            {
                filteredProblems = null;
                reasoningContext = null;
                return;
            }

            var filteredProblemList = new List<ClinicalProblemDefinition>();
            var unioncontext = new Context();
            foreach (var problem in candidateProblems)
            {
                var context = new Context();
                foreach (var contextItem in problem.ContextItemDefinition)
                {
                    if (contextItem.DataType == EnumDataType.数值型.ToString())
                    {
                        context.NumericParameters.Add(contextItem.Name, 0);
                    }
                    else if (contextItem.DataType == EnumDataType.布尔型.ToString())
                    {
                        context.BooleanParameters.Add(contextItem.Name, false);
                    }
                    else
                    {
                        context.StringParameters.Add(contextItem.Name, "");
                    }
                }

                // Judge whether all items are available
                if (FillReasoningContext(encounter, ref context) == true)
                {
                    filteredProblemList.Add(problem);
                    unioncontext = unioncontext + context; // "+" is a union operation
                }
            }
            filteredProblems = filteredProblemList.ToList();
            reasoningContext = unioncontext.Clone() as Context;
        }

        /// <summary>
        /// Fill reasoning context
        /// </summary>
        /// <param name="encounter">patient encounter, where data come from its profile</param>
        /// <param name="context">in out parameter</param>
        /// <returns>true if successful; false if any field is unavailable</returns>
        public bool FillReasoningContext(Encounter encounter, ref Context context)
        {
            if (encounter == null ||
                encounter == null ||
                encounter.Fact.Count() <= 0)
            {
                return false;
            }

            // Cannot use ref or out parameter 'context' inside an anonymous method, lambda expression, or query expression.
            // Therefore, create a copy.
            var ctx = context.Clone() as Context;

            // indicate whether each fact is set value
            var flags = new Dictionary<string, bool>();
            var keys = ctx.GetAllKeys();
            keys.ToList().ForEach(x => flags.Add(x, false));

            encounter.Fact.ToList().ForEach(x =>
            {
                if (keys.Contains(x.ContextItemDefinition.Name))
                {
                    if (ctx.SetValue(x) == true)
                        flags[x.ContextItemDefinition.Name] = true;
                }
            });

            // data is incomplete
            if (flags.ContainsValue(false))
                return false;

            context = null;
            context = ctx;
            return true;
        }

        /// <summary>
        /// Innter Implemention of I/F Notify():
        /// void IDecisionSupportService.Notify(int id)
        /// </summary>
        /// <param name="id">event id</param>
        /// <returns>log items.
        /// For each log keypair, DateTime is timestamp, string is log content.</returns>
        public IEnumerable<KeyValuePair<DateTime, string>> Notify(int id)
        {
            var log = new List<KeyValuePair<DateTime, string>>();
            var sw = Stopwatch.StartNew();
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "[BEGIN]"));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "Call I/F Notify(" + id + ")"));

            // TODO: Search EMR db and construct event, report, encounter, etc. in CDS db

            var evt = db.Event.Find(id);
            if (evt == null)
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "Event with specified id doesnot exist. Exit funciton now."));
                return log;
            }

            var encounter = evt.Encounter;
            if (encounter == null)
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "Associated encounter doesnot exist. Exit funciton now."));
                return log;
            }

            // Collect abnormal facts from the event
            //            
            var abnormalFacts = new List<Fact>();
            var relatedProblemInstances = new List<ClinicalProblemInstance>();
            var solvedSuspectProblemInstances = new List<ClinicalProblemInstance>();
            foreach (var x in evt.Report)
            {
                // Get facts from report and update to profile.
                var facts = (new FactServiceMocker()).GetFactsFromReportMock(x.Id, db).ToList();
                // TODO: var facts = factService.GetFactsFromReport(x.URL).ToList();
                facts.ForEach(y =>
                {
                    db.UpdateFactCache(encounter.Id, y);
                    if (y.ContextItemDefinition.Type == EnumContextItemType.ClinicalProblem.ToString() && y.IsAbnormal == false)
                    {
                        var instance = db.ClinicalProblemInstance.FirstOrDefault(z => z.State == EnumProblemState.New.ToString() && z.ClinicalProblemDefinition.Name == y.ContextItemDefinition.Name);
                        if (instance != null)
                        {
                            solvedSuspectProblemInstances.Add(instance);
                        }
                    }

                    // collect related problem instances associated with the fact. Later, use Distinct() to get full list.
                    foreach (var z in db.ClinicalProblemInstance)
                    {
                        if ((z.State == EnumProblemState.New.ToString() ||
                            z.State == EnumProblemState.ResolvedSuspected.ToString()) &&
                            z.ClinicalProblemDefinition.ContextItemDefinition.Contains(y.ContextItemDefinition, new PropertyComparer<ContextItemDefinition>("Id")))
                        {
                            relatedProblemInstances.Add(z);
                        }
                    }

                    // Collect abnormal facts as triggers
                    if (y.IsAbnormal == true)
                    {
                        abnormalFacts.Add(y);
                    }
                });
            };

            // Update problems that are suspected no longer existing.
            solvedSuspectProblemInstances.Distinct(new PropertyComparer<ClinicalProblemInstance>("Id")).ToList().ForEach(x =>
            {
                db.UpdateProblemState(x.Id, EnumProblemState.ResolvedSuspected.ToString(), EnumProblemStateChangeReason.CDS.ToString(), CurrentUser);
            });

            // Update related active problems with new report, for tracking.
            relatedProblemInstances.Distinct(new PropertyComparer<ClinicalProblemInstance>("Id")).ToList().ForEach(x =>
            {
                var result = ReEvaluate(x.Id);
                if (result.HasValue)
                {
                    if (result.Value == false)
                    {
                        db.UpdateProblemState(x.Id, EnumProblemState.ResolvedSuspected.ToString(), EnumProblemStateChangeReason.CDS.ToString(),CurrentUser);
                    }
                    else
                    {
                        db.UpdateProblemState(x.Id, x.State, EnumProblemStateChangeReason.CDS.ToString(),CurrentUser);
                    }
                }
            });

            if (abnormalFacts.Count <= 0)
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "No abnormal facts were detected. Exit funciton now."));
                return log;
            }

            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, abnormalFacts.Count + " abnormal facts were detected:"));
            abnormalFacts.ForEach(x =>
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, x.ContextItemDefinition.Name + " = " + x.ValueString()));
            });
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));

            var triggerFacts = new List<Fact>();
            abnormalFacts.ForEach(x =>
            {
                if (x.ContextItemDefinition.Type==EnumContextItemType.ClinicalProblem.ToString()) // The fact itself is a problem. e.g. the fact comes from a diagnosis event.
                {
                    int ID = db.CreateClinicalProblemInstance(x.ContextItemDefinition.Name, encounter.Id, false);
                    if (ID != -1)
                    {
                        db.UpdateProblemState(ID, EnumProblemState.New.ToString(), EnumProblemStateChangeReason.CDS.ToString(), CurrentUser, true);
                        log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "!!! New Problem[" + x.ContextItemDefinition.Name + "] is detected directly from fact[" + x.ContextItemDefinition.Name + "=" + x.ValueString() + "]. (the fact itself is a problem)"));
                    }
                }
                else // The fact needs reasoning by rule engine to generate problem.
                {
                    triggerFacts.Add(x);
                }
            });

            //
            // Get candidate problems by abnormal facts
            //
            var candidateProblems = GetCandidateProblems(triggerFacts);

            if (candidateProblems.Count() <= 0)
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "No candidate problems were detected. Exit function now."));
                return log;
            }

            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, candidateProblems.Count() + " candidate problems were detected:"));
            candidateProblems.ToList().ForEach(x =>
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, x.Name));
            });
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));


            //
            // Construct reasoning context for candidate problems
            //
            IEnumerable<ClinicalProblemDefinition> filteredProblems;
            Context reasoningContext;
            ConstructReasoningContext(encounter, candidateProblems, out filteredProblems, out reasoningContext);

            if (filteredProblems.Count() <= 0)
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "Lacking reasoning facts for all candidate problems. Exit function now."));
                return log;
            }

            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "After checking data availability, " + filteredProblems.Count() + " problems can go through rule engine:"));
            filteredProblems.ToList().ForEach(x =>
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, x.Name));
            });
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "Reasoning context contains: " + reasoningContext.GetAllKeys().Count() + " items:"));
            foreach (var x in reasoningContext.GetAllKeys())
            {
                log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, x + " = " + reasoningContext.GetValue(x)));
            }
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));

            //
            // Reason by Rule Engine
            //
            var triggeredProblems = Reason(filteredProblems, reasoningContext);

            if (triggeredProblems != null && triggeredProblems.Count() > 0)
            {
                triggeredProblems.ToList().ForEach(x =>
                {
                    int ID = db.CreateClinicalProblemInstance(x, encounter.Id, true);
                    if (relatedProblemInstances.Any(y => y.Id == ID) == false // means the problem state is already updated above
                        && ID != -1)
                    {
                        db.UpdateProblemState(ID, EnumProblemState.New.ToString(), EnumProblemStateChangeReason.CDS.ToString(), CurrentUser);
                        var instance = db.ClinicalProblemInstance.Find(ID);
                        if (instance != null && instance.TriggerRule != null && string.IsNullOrEmpty(instance.TriggerRule.RuleSet) == false)
                            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "!!! New Problem[" + x + "] is detected from rule[" + RuleSetHelper.GetLaymanConditionString(instance.TriggerRule.RuleSet) + "]."));
                    }
                });
            }

            sw.Stop();
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, ""));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "This I/F call consumes " + sw.ElapsedMilliseconds + " ms"));
            log.Add(new KeyValuePair<DateTime, string>(DateTime.Now, "[END]"));

            return log;
        }

        /// <summary>
        /// Run the problem instance through rule engine AGAIN, to check whether the problem persist.
        /// If no longer exist, should call UpdateClinicalProblemInstanceState() to udpate state.
        /// </summary>
        /// <param name="id">Problem Instance ID</param>
        /// <returns>true if still exist; false if not exist; null if reasoning cannot be performed</returns>
        public bool? ReEvaluate(int id)
        {
            var instance = db.ClinicalProblemInstance.Find(id);
            if (instance == null ||
                instance.ClinicalProblemDefinition == null ||
                instance.Encounter == null ||
                instance.ClinicalProblemDefinition.ContextItemDefinition == null)
                return null;

            IEnumerable<ClinicalProblemDefinition> filteredProblems;
            Context reasoningContext;
            ConstructReasoningContext(instance.Encounter,
                new List<ClinicalProblemDefinition>() { instance.ClinicalProblemDefinition },
                out filteredProblems, out reasoningContext);

            if (filteredProblems == null || filteredProblems.Count() <= 0)
                return null;

            var triggeredProblems = this.Reason(filteredProblems, reasoningContext);

            return triggeredProblems.Contains(instance.ClinicalProblemDefinition.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ClinicalProblemInstance Id</param>
        /// <param name="operation">EnumStateTransition</param>
        /// <param name="user"></param>
        public void UpdateProblemState(int id, string operation, string user=null)
        {
            var instance = db.ClinicalProblemInstance.Find(id);
            if (instance == null)
                return;

            if (string.IsNullOrEmpty(operation))
                return;

            string usr = user;
            if (string.IsNullOrEmpty(user))
            {
                usr = CurrentUser;
            }

            if (operation == EnumStateTransition.Solve.ToString())
            {
                if (instance.State == EnumProblemState.New.ToString() ||
                    instance.State == EnumProblemState.ResolvedSuspected.ToString())

                {
                    db.UpdateProblemState(id, EnumProblemState.Resolved.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), usr);
                }
            }
            else if (operation == EnumStateTransition.Dismiss.ToString())
            {
                if (instance.State == EnumProblemState.New.ToString() ||
                    instance.State == EnumProblemState.ResolvedSuspected.ToString())
                {
                    db.UpdateProblemState(id, EnumProblemState.Dismissed.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), usr);
                }
            }
            else if (operation == EnumStateTransition.Revive.ToString())
            {
                if (instance.State == EnumProblemState.Dismissed.ToString())
                {
                    db.UpdateProblemState(id, EnumProblemState.New.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), usr);
                }
            }            
        }

#if false
        
        /// <summary>
        /// Notify decision support system that a new event has occured. 
        /// The new event and related report is saved to DB before this calling, by EMR/CPOE systems.
        /// [TODO]
        /// CASE1: Notify Visit ID
        /// CASE2: Notify Test ID
        /// </summary>
        /// <param name="message">integration message. Message details should be negotiated.</param>
        /// <remarks>It is advisable that report and trigger DB tables be periodically purged. Only the copies of those that have triggered problems are kept.</remarks>
        void IDecisionSupportService.Notify(string message)
        {
            // if message is new_encounter
            var xml = (new DataServiceMocker()).GetPatientAndEncounterInfo(message);
            // Convert xml to patient and encounter, and populate DB
            // else if message is new_event
            xml = (new DataServiceMocker()).GetEventAndReportInfo(message);
            // Convert xml to event and report, and populate DB
            // Notify(event_id)
        }
               

        #region "Following function are logics corresponding to upper client UI"

        // 
        // solved. put into inactive region.
        // [TODO] Add this logic in upper client.
        private void ButtonClick_Solve(int id)
        {
            var result= ReEvaluate(id);
            if (result.HasValue)
            {
                if (result.Value == false)
                {
                    // For this case, update reports.
                    db.UpdateProblemState(id, EnumProblemState.Resolved.ToString(), EnumProblemStateChangeReason.CDS.ToString(), CurrentUser);
                }
                else
                {
                    //[TODO]
                    // There is no report indicating the problem is gone. Are you sure to solve it?
                    // yes
                    db.UpdateProblemState(id, EnumProblemState.Resolved.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), CurrentUser);
                }
            }
        }

        
        //
        // mark. Mark as important/urgent/serious. This will cause color change.
        private void ButtonClick_Mark(int id)
        {
            var instance = db.ClinicalProblemInstanceContextItemDefinitionFind(id);
            if (instance != null)
                instance.Priority = EnumProblemPriority.High.ToString();
        }

        //
        // neglect. Mark as unimportant/inserious. 
        private void ButtonClick_Downplay(int id)
        {
            var instance = db.ClinicalProblemInstanceContextItemDefinitionFind(id);
            if (instance != null)
                instance.Priority = EnumProblemPriority.Low.ToString();
        }

        //
        // This will cause color change back to normal.
        private void ButtonClick_EraseMark(int id)
        {
            var instance = db.ClinicalProblemInstanceContextItemDefinitionFind(id);
            if (instance != null)
                instance.Priority = EnumProblemPriority.Middle.ToString();
        }

        #endregion

        /// <summary>
        /// Retrieve the list of clinical problems for a patient encounter.
        /// </summary>
        /// <param name="id">encounter id</param>
        /// <returns>An xml serializtion string of a list of clinical problems</returns>
        string IDecisionSupportService.RetrieveProblemList(int id)
        {
            var encounter = db.EncounterContextItemDefinitionFind(id);
            if (encounter == null)
                return "";
            try
            {
                var list = new List<ClinicalProblemInstanceSimpleObject>();

                foreach (var instance in AssociationHelper.SearchClinicalProblemInstance(encounter, db))
                {

                    var simpleObject = new ClinicalProblemInstanceSimpleObject()
                    {                        
                        Reports = new List<KeyValuePair<string, string>>(),
                        Facts = new List<string>(),
                        OrderSet = new List<string>(),
                        History = new List<string>()
                    };

                    simpleObject.Id = instance.Id;
                    simpleObject.Priority = instance.Priority;
                    simpleObject.State = instance.State;
                    if(instance.ClinicalProblemDefinition!=null && string.IsNullOrEmpty(instance.ClinicalProblemDefinition.Name)==false)
                        simpleObject.Name = instance.ClinicalProblemDefinition.Name;
                    if(instance.ChangeRecord!=null && instance.ChangeRecord.Count()>0)
                        simpleObject.TimeStamp = instance.ChangeRecord.ElementAt(0).TimeStamp.Value;
                    if (instance.ClinicalProblemDefinition != null && instance.ClinicalProblemDefinition.TriggerRule != null)
                        simpleObject.TriggerRule = RuleSetHelper.GetLaymanConditionString(instance.ClinicalProblemDefinition.TriggerRule.RuleSet);

                    foreach (var fact in instance.Facts)
                    {
                        var factString = string.IsNullOrEmpty(fact.ContextItemDefinition.ReferenceRange) ?
                            fact.ContextItemDefinition.Name + " = " + fact.ValueString() :
                            fact.ContextItemDefinition.Name + " = " + fact.ValueString() + " (参考范围:" + fact.ContextItemDefinition.ReferenceRange + ")";
                        simpleObject.Facts.Add(factString);
                    }

                    foreach (var report in instance.Reports)
                    {
                        simpleObject.Reports.Add(new KeyValuePair<string, string>(report.TimeStamp + " " + report.ReportType, report.URL));
                    }

                    // Don't need to show history for now.
                    //foreach (var record in instance.ChangeRecord)
                    //{
                    //    simpleObject.History.Add();
                    //}

                    int count = 0;
                    foreach (var order in db.MedicalOrderSet)
                    {
                        simpleObject.OrderContextItemDefinitionAdd(order.Name);
                        if (count++ > 10)
                            break;
                    }

                    list.Add(simpleObject);
                }                

                var obj = list as object;
                return SerializationHelper.Serialize(ref obj);
            }
            catch
            {
                return "";
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">problem instance id</param>
        /// <param name="operation">operation code</param>
        /// <param name="priority">priority code</param>
        void IDecisionSupportService.UpdateProblemState(int id, string operation, string priority)
        {
            var instance = db.ClinicalProblemInstanceContextItemDefinitionFind(id);
            if (instance == null)
                return;

            //
            // Update state
            if (operation == EnumStateTransition.Solve.ToString())
            {
                db.UpdateProblemState(id, EnumProblemState.Resolved.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), CurrentUser);
            }
            else if (operation == EnumStateTransition.Dismiss.ToString())
            {
                db.UpdateProblemState(id, EnumProblemState.Dismissed.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), CurrentUser);
            }
            else if (operation == EnumStateTransition.Revive.ToString())
            {
                db.UpdateProblemState(id, EnumProblemState.New.ToString(), EnumProblemStateChangeReason.UserDecision.ToString(), CurrentUser);
            }
            else if (operation == EnumStateTransition.Create.ToString())
            {
                // NOTE: Here, for consitency, we don't allow user to CREATE problem. 
                // User should provide report, such as a diagnosis report and fire an event to let CDS generate the problem.
            }

            //
            // Update priority
            EnumProblemPriority value;
            if (Enum.TryParse<EnumProblemPriority>(priority, out value))
            {
                instance.Priority = value.ToString();
            }
        }

#if false
        /// <summary>
        /// Design A
        /// </summary>
        /// <param name="id">plan instance id</param>
        /// <param name="operation"></param>
        /// <param name="phase"></param>
        /// <param name="checkedOrderItems"></param>
        /// <remarks>NOTE: In order to make plan instance id always available. When create a problem instance, its associated plan instances should also be created. CPOE can also use the plan instance to acess plan templates.
        But when apply one orderset multiple times, CPOE is responsible to create multiple plan instances. That's complex.</remarks>
        void UpdatePlanState(int id, string operation, string phase, IEnumerable<string> checkedOrderItems)
        {
            var planInstance = db.PlanInstanceContextItemDefinitionFind(id);
            if (planInstance == null)
                return;

            //
            // Update state
            if (operation == EnumStateTransition.Apply.ToString())
            {
                planInstance.State = EnumPlanState.Applied.ToString();
                planInstance.CurrentPhase = phase;
                planInstance.GetMedicalOrderInstance(db).Populate with checkedOrderItems;                
            }
            ...
        }
#endif

        /// <summary>
        /// PlanDefinition B
        /// </summary>
        /// <param name="id">problem instance id</param>
        /// <param name="plan">plan name</param>
        /// <param name="operation"></param>
        /// <param name="phase"></param>
        /// <param name="checkedOrderItems"></param>
        void UpdatePlanState(int id, string plan, string operation, string phase, IEnumerable<string> checkedOrderItems)
        {
            var instance = db.ClinicalProblemInstanceContextItemDefinitionFind(id);
            if (instance == null)
                return;

            //
            // Update state
            if (operation == EnumStateTransition.Apply.ToString())
            {
                var planInstance = new PlanInstance();
                planInstance.PlanDefinition = db.PlanContextItemDefinitionFirstOrDefault(x => x.Name == plan);
                planInstance.CurrentPhase = phase;
                planInstance.State = EnumPlanState.Applied.ToString();
                //planInstance.GetMedicalOrderInstance(db).Populate with checkedOrderItems;
            }
        }

        /// <summary>
        /// Periodically, CDS should update lab test dict from EMR/CDR.
        /// </summary>
        /// <remarks>Use decimal here, though not 100% necessary.
        /// decimal for when you work with values in the range of 10^(+/-28) and where you have expectations about the behaviour based on base 10 representations - basically money.
        /// double for when you need relative accuracy (i.e. losing precision in the trailing digits on large values is not a problem) across wildly different magnitudes - double covers more than 10^(+/-300). Scientific calculations are the best example here.
        /// </remarks>
        public void UpdateLabTestDict()
        {
            // Upon intialization, Lab Test Dict is loaded from standard dict 临床检验项目.xls. 
            db.ClearLabTestDict();

            RuleContainerInitializer.SeedLabTest("local");
        }

        /// <summary>
        /// Not so frequently, CDS should update problem dict from EMR/CDR.
        /// Data are from [DIAGNOSIS_DICT] table
        /// </summary>
        public void UpdateProblemDict()
        {
            for (int i = db.ClinicalProblemContextItemDefinitionCount(); i > 0; i--)
            {
                db.ClinicalProblemContextItemDefinitionRemove(db.ClinicalProblemContextItemDefinitionElementAt(i));
            }
            db.SaveChanges();

            RuleContainerInitializer.SeedProblem("local");
        }

        /// <summary>
        /// Not so frequently, CDS should update vital signs dict from EMR/CDR.
        /// Data are from [VITAL_SIGNS_DICT], etc table
        /// </summary>
        public void UpdatePhysiologicalItemDict()
        {
            db.ClearPhysiologicalItemDict();

            RuleContainerInitializer.SeedPhysiologicalItems("local");             
        }
#endif

        public void Notify(string message)
        {
            throw new NotImplementedException();
        }

        public void UpdateLabTestDict()
        {
            throw new NotImplementedException();
        }

        public void UpdateProblemDict()
        {
            throw new NotImplementedException();
        }

        public void UpdatePhysiologicalItemDict()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentUser(string user)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentEncounter(string id)
        {
            throw new NotImplementedException();
        }
        
        public void AddProblem(int encounter_id, int problem_definition_id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProblemState(int id, string operation)
        {
            UpdateProblemState(id, operation, null);
        }
    }
}
