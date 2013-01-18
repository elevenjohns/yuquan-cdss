//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

namespace WuKeSong.Services
{
    public sealed partial class TriggerRuleWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.faultHandlersActivity = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.policyFromService = new PolicyFromService();
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity.Name = "faultHandlersActivity";
            // 
            // Workflow1
            // 
            this.Activities.Add(this.policyFromService);
            this.Activities.Add(this.faultHandlersActivity);
            this.Name = "TriggerRuleWorkflow";
            this.Completed += new System.EventHandler(this.WorkflowCompleted);
            this.CanModifyActivities = false;

        }

        #endregion

        private System.Workflow.ComponentModel.FaultHandlersActivity faultHandlersActivity;
        private PolicyFromService policyFromService;
    }
}

