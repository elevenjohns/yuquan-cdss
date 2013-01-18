//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Workflow.Activities;
using YuQuan.Models;

namespace WuKeSong.Services
{
    public sealed partial class TriggerRuleWorkflow : SequentialWorkflowActivity
    {
        public TriggerRuleWorkflow()
        {
            InitializeComponent();

            // 
            // policyFromService1
            // 
            this.policyFromService.Name = "policyFromService";
            this.policyFromService.RuleSetName = RuleSetName;
            this.Context = (Context)ExchangeContext.Clone();
        }
        
        private ICollection<ClinicalProblemInstance> _problems = new Collection<ClinicalProblemInstance>();
        // public ICollection<ClinicalProblemInstance> Problems { get { return _problems; } set { _problems = value; } }

        public static Context ExchangeContext = new Context();
        public Context Context { get; set; }
        public static DateTime PushContextTimeStamp = DateTime.Now;
        public static string RuleSetName=String.Empty;
        public static ICollection<string> Problems = new Collection<string>();


        // event --> ... --> problems
        // RetriveContext as WS
        // evidence collection for each problem

        private void WorkflowCompleted(object sender, EventArgs e)
        {
            // log
            using (StreamWriter sw = File.AppendText(@"c:\workflow.log"))
            {
                sw.WriteLine(DateTime.Now.ToString() + ": " + "Worflow Finished!");
                if(Problems.Count>0)
                    sw.WriteLine("Following Clinical Problems were detected:");
                foreach (var item in Problems)
                {                    
                    sw.WriteLine(item);
                }
            }
        }

        ~TriggerRuleWorkflow()
        {
            using (StreamWriter sw = File.AppendText(@"c:\workflow.log"))
            {
                sw.WriteLine(DateTime.Now.ToString() + ": " + "Worflow Destructed!");
            }
        }
     }
}

