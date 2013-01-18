using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using YuQuan.Models;

namespace YuQuan.Models
{
    public partial class KBEntities
    {
        public void AddComplexObject(PlanDefinition plan)
        {
            try
            {
                foreach (var phase in plan.PhaseDefinition)
                {
                    foreach (var task in phase.TaskDefinition)
                    {
                        foreach (var medicalOrder in task.MedicalOrderDefinition)
                        {
                            this.MedicalOrderDefinition.Add(medicalOrder);
                        }
                        this.TaskDefinition.Add(task);
                    }
                    this.PhaseDefinition.Add(phase);
                }
                this.PlanDefinition.Add(plan);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                Trace.TraceInformation("DbUpdateException" + dbEx.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.Message);
            }
        }

        public void DeleteComplexObject(PlanDefinition plan)
        {
            try
            {
                for (int i=plan.PhaseDefinition.Count-1;i>=0;i--)
                {
                    var phase = plan.PhaseDefinition.ToList()[i];
                    for (int j = phase.TaskDefinition.Count - 1; j >= 0; j--)
                    {
                        var task = phase.TaskDefinition.ToList()[j];
                        for (int k = task.MedicalOrderDefinition.Count - 1; k >= 0; k--)
                        {
                            this.MedicalOrderDefinition.Remove(task.MedicalOrderDefinition.ToList()[k]);
                        }
                        this.TaskDefinition.Remove(task);
                    }
                    this.PhaseDefinition.Remove(phase);
                }
                this.PlanDefinition.Remove(plan);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                Trace.TraceInformation("DbUpdateException" + dbEx.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.Message);
            }
        }

        public void DeleteComplexObject(PhaseDefinition phase)
        {
            try
            {
                for (int j = phase.TaskDefinition.Count - 1; j >= 0; j--)
                {
                    var task = phase.TaskDefinition.ToList()[j];
                    for (int k = task.MedicalOrderDefinition.Count - 1; k >= 0; k--)
                    {
                        this.MedicalOrderDefinition.Remove(task.MedicalOrderDefinition.ToList()[k]);
                    }
                    this.TaskDefinition.Remove(task);
                }
                this.PhaseDefinition.Remove(phase);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                Trace.TraceInformation("DbUpdateException" + dbEx.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.Message);
            }
        }
        
        public void DeleteComplexObject(TaskDefinition task)
        {
            try
            {
                for (int k = task.MedicalOrderDefinition.Count - 1; k >= 0; k--)
                {
                    this.MedicalOrderDefinition.Remove(task.MedicalOrderDefinition.ToList()[k]);
                }
                this.TaskDefinition.Remove(task);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                Trace.TraceInformation("DbUpdateException" + dbEx.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.Message);
            }
        }

        /// <summary>
        /// Create a ClinicalProblemInstance for a patient encounter, and save to DB.
        /// </summary>
        /// <param name="name">Problem Name</param>
        /// <param name="id">Encounter ID</param>
        /// <param name="ruleInduced">Whether this problem instance was induced by rule set. Some problems are created directly from diagnosis events, etc, and don't need reasoning rule set.</param>
        /// <returns>The created ClinicalProblemInstance ID.
        /// -1 if failed</returns>
        public int CreateClinicalProblemInstance(string name, int id, bool ruleInduced /*=false*/)
        {
            if (string.IsNullOrEmpty(name))
                return -1;

            var encounter = this.Encounter.Find(id);
            if (encounter == null)
                return -1;

            var status = EnumItemStatus.Effective.ToString();
            var problem = this.ClinicalProblemDefinition.FirstOrDefault(x => x.Name == name && x.Status == status);
            if (problem == null)
                return -1;

            // If the problem instance is already generated, just return its id.
            // However, problem instances with same name but in different states are allowed to exist.
            var activeState1 = EnumProblemState.New.ToString();
            var activeState2 = EnumProblemState.ResolvedSuspected.ToString();
            var instance = this.ClinicalProblemInstance.FirstOrDefault(x => 
                (x.ClinicalProblemDefinition.Name == name && 
                x.Encounter.Id == id && 
                (x.State==activeState1||x.State==activeState2 || x.State==null)));
            if (instance != null)
                return instance.Id;

            var problemInstance = new ClinicalProblemInstance();
            //
            // Problem instance's state is set in UpdateClinicalProblemInstanceState()
            //
            problemInstance.State = null;//EnumProblemState.New.ToString();
            problemInstance.ClinicalProblemDefinition = problem;
            problemInstance.Encounter = encounter;            
            problemInstance.TriggerRule = ruleInduced?problem.TriggerRule:null;//NOTE: no TriggerRule should be deleted. Only add new versions.
            // If ruleInduced == false, then it is a user added problem (via diagnosis event). We always mark such problems in high priority.
            problemInstance.Priority = ruleInduced ? EnumProblemPriority.Middle.ToString() : EnumProblemPriority.High.ToString();         

            this.ClinicalProblemInstance.Add(problemInstance);
            this.SaveChanges();
            return problemInstance.Id;
        }

        /// <summary>
        /// The Fact Cache is a profile for an encounter, which contains latest fact for each context item.
        /// </summary>
        /// <param name="id">encounter id</param>
        /// <param name="fact">the fact created externally</param>
        public void UpdateFactCache(int id, Fact fact)
        {
            var encounter = this.Encounter.Find(id);

            if (fact == null || fact.ContextItemDefinition == null)
                return;

            var matchedFact = encounter.Fact.FirstOrDefault(x => x.ContextItemDefinition.Id == fact.ContextItemDefinition.Id); // match use id, not name, because there are same-name items of different versions.
            if (matchedFact != null)
            {
                // matchedFact = Models.Fact.MemberwiseClone(fact);
                // this.Entry(matchedFact).State = System.Data.EntityState.Modified;                
                //matchedFact.Confidentiality = fact.Confidentiality;
                //matchedFact.Report = fact.Report;
                //matchedFact.IsAbnormal = fact.IsAbnormal;
                //matchedFact.BooleanValue = fact.BooleanValue;
                //matchedFact.NumericValue = fact.NumericValue;
                //matchedFact.StringValue = fact.StringValue;
                matchedFact.Assign(fact);
                SaveChanges();
            }
            else
            {
                this.Fact.Add(fact);
                encounter.Fact.Add(fact);
                SaveChanges();
            }
        }

        ///// <summary>
        ///// Create a new ChangeRecord for problem instance, and update its state field.
        ///// We allow update with a same state in which case is meant to update fact and report.
        ///// </summary>
        ///// <param name="id">ClinicalProblemInstance ID</param>
        ///// <param name="state">New State</param>        
        ///// <param name="user">User/Operator Name</param>
        ///// <param name="fromDiagnosis">If true, means this problem is generated directly by a Problem Fact, i.e. from diagnosis. In this case, reasoning is not needed.</param>
        //public void UpdateProblemState(int id, string state,string reason="CDS",string user="",bool fromDiagnosis=false)
        //{
        //    var instance = this.ClinicalProblemInstance.Find(id);
        //    if (instance == null || 
        //        // instance.State == state || // Sometimes, we update reports for the same state
        //        instance.ClinicalProblemDefinition == null || 
        //        this.ClinicalProblemDefinition_ContextItemDefinition_AssociationSet.Count(x=>x.ClinicalProblemDefinition_Id == instance.ClinicalProblemDefinition.Id) == 0 ||
        //        instance.Encounter==null 
        //        //|| instance.Encounter.Profile ==null||
        //        //instance.Encounter.Profile.Fact == null
        //        )
        //        return;


        //    // Add a history record to track state change
        //    var record = new ChangeRecord()
        //    {
        //        OldState = instance.State,
        //        NewState = state,
        //        TimeStamp = DateTime.Now,
        //        Operator = user,
        //        Reason = reason
        //    };

        //    // If the state change is fired by CDS, then update related reports.
        //    if (reason == EnumProblemStateChangeReason.CDS.ToString() &&
        //        instance.Encounter!=null &&
        //        instance.Encounter.Fact != null)
        //    {
        //        if (fromDiagnosis == false)
        //        {
        //            this.ClinicalProblemDefinition_ContextItemDefinition_AssociationSet.Where(x=>x.ClinicalProblemDefinition_Id == instance.ClinicalProblemDefinition.Id).Select(x=>x.ContextItemDefinition_Id).ToList().ForEach(x=>
        //            //instance.ClinicalProblemDefinition.ContextItemDefinition.ToList().ForEach(x =>
        //            {
        //                var fact = instance.Encounter.Fact.FirstOrDefault(y => y.ContextItemDefinition.Id == x);
        //                if (fact != null)
        //                {
        //                    Trace.WriteLine("UpdateProblemState: " + fact.ContextItemDefinition.Name + " = " + fact.ValueString());
                            
        //                    var copiedFact = Fact.Clone(fact);
        //                    //var copiedFact = new Fact()
        //                    //{
        //                    //    BooleanValue = fact.BooleanValue,
        //                    //    Confidentiality = fact.Confidentiality,
        //                    //    ContextItemDefinition = fact.ContextItemDefinition,
        //                    //    Report = fact.Report,
        //                    //    IsAbnormal = fact.IsAbnormal,
        //                    //    LifeSpan = fact.LifeSpan,
        //                    //    NumericValue = fact.NumericValue,
        //                    //    StringValue = fact.StringValue
        //                    //};
                            
        //                    copiedFact.LifeSpan = EnumLifeSpan.Persistent.ToString();
        //                    record.Fact.Add(copiedFact);
        //                    this.Fact.Add(copiedFact);
        //                }
        //            });
        //        }
        //        else
        //        {
        //            var fact = instance.Encounter.Fact.FirstOrDefault(y => y.ContextItemDefinition.Name == instance.ClinicalProblemDefinition.Name);
        //            if (fact != null)
        //            {
        //                var copiedFact = Fact.Clone(fact);
        //                copiedFact.LifeSpan = EnumLifeSpan.Persistent.ToString();
        //                record.Fact.Add(copiedFact);
        //                this.Fact.Add(copiedFact);
        //            }
        //        }
        //    }

        //    this.ChangeRecord.Add(record);
        //    instance.State = state;
        //    instance.ChangeRecord.Add(record);

        //    this.SaveChanges();
        //}
                
        public void ClearContextItemDefinitionDict()
        {
            for (int i = this.ContextItemDefinition.Count(); i > 0; i--)
            {
                this.ContextItemDefinition.Remove(this.ContextItemDefinition.ElementAt(i));
            }
            this.SaveChanges();            
        }

        /// <summary>
        /// Create a new ChangeRecord for problem instance, and update its state field.
        /// We allow update with a same state in which case is meant to update fact and report.
        /// </summary>
        /// <param name="id">ClinicalProblemInstance ID</param>
        /// <param name="state">New State</param>        
        /// <param name="user">User/Operator Name</param>
        /// <param name="fromDiagnosis">If true, means this problem is generated directly by a Problem Fact, i.e. from diagnosis. In this case, reasoning is not needed.</param>
        public void UpdateProblemState(int id, string state, string reason = "CDS", string user = "", bool fromDiagnosis = false)
        {
            var instance = this.ClinicalProblemInstance.Find(id);
            if (instance == null ||
                // instance.State == state || // Sometimes, we update reports for the same state
                instance.ClinicalProblemDefinition == null ||
                instance.ClinicalProblemDefinition.ContextItemDefinition == null ||
                instance.Encounter == null
                //|| instance.Encounter.Profile ==null||
                //instance.Encounter.Profile.Fact == null
                )
                return;


            // Add a history record to track state change
            var record = new ChangeRecord()
            {
                OldState = instance.State,
                NewState = state,
                TimeStamp = DateTime.Now,
                Operator = user,
                Reason = reason
            };

            // If the state change is fired by CDS, then update related reports.
            if (reason == EnumProblemStateChangeReason.CDS.ToString() &&
                instance.Encounter.Fact != null)
            {
                if (fromDiagnosis == false)
                {
                    instance.ClinicalProblemDefinition.ContextItemDefinition.ToList().ForEach(x =>
                    {
                        var fact = instance.Encounter.Fact.FirstOrDefault(y => y.ContextItemDefinition.Id == x.Id);
                        if (fact != null)
                        {
                            Trace.WriteLine("UpdateProblemState: " + fact.ContextItemDefinition.Name + " = " + fact.ValueString());

                            var copiedFact = new Fact();
                            copiedFact.Assign(fact);

                            //var copiedFact = new Fact()
                            //{
                            //    BooleanValue = fact.BooleanValue,
                            //    Confidentiality = fact.Confidentiality,
                            //    ContextItem = fact.ContextItem,
                            //    Report = fact.Report,
                            //    IsAbnormal = fact.IsAbnormal,
                            //    LifeSpan = fact.LifeSpan,
                            //    NumericValue = fact.NumericValue,
                            //    StringValue = fact.StringValue
                            //};

                            copiedFact.LifeSpan = EnumLifeSpan.Persistent.ToString();
                            record.Fact.Add(copiedFact);
                            this.Fact.Add(copiedFact);
                        }
                    });
                }
                else
                {
                    var fact = instance.Encounter.Fact.FirstOrDefault(y => y.ContextItemDefinition.Name == instance.ClinicalProblemDefinition.Name);
                    if (fact != null)
                    {
                        var copiedFact = new Fact();
                        copiedFact.Assign(fact);
                        copiedFact.LifeSpan = EnumLifeSpan.Persistent.ToString();
                        record.Fact.Add(copiedFact);
                        this.Fact.Add(copiedFact);
                    }
                }
            }

            this.ChangeRecord.Add(record);
            instance.State = state;
            instance.ChangeRecord.Add(record);

            this.SaveChanges();
        }
    }
}