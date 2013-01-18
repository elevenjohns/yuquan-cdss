using System.Drawing;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Activities;
using System.Workflow.ComponentModel.Compiler;
namespace WuKeSong.Services
{
    [ToolboxBitmapAttribute(typeof(PolicyActivity), "Resources.Rule.png")]
    [ToolboxItemAttribute(typeof(ActivityToolboxItem))]
    public partial class PolicyFromService : System.Workflow.ComponentModel.Activity
    {
        public PolicyFromService()
        {
            InitializeComponent();
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var ruleSetService = context.GetService<RuleSetService>();

            if (ruleSetService != null)
            {
                RuleSet ruleSet = ruleSetService.GetRuleSet(this.RuleSetName);
                if (ruleSet != null)
                {
                    Activity targetActivity = this.GetRootWorkflow(this.Parent);
                    RuleValidation validation = new RuleValidation(targetActivity.GetType(), null);
                    RuleExecution execution = new RuleExecution(validation, targetActivity, context);
                    try
                    {
                        ruleSet.Execute(execution);
                    }
                    catch(RuleSetValidationException ex)
                    {
                        string str = ex.Message;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("A RuleSetService must be configured on the host to use the PolicyFromService activity.");
            }

            return ActivityExecutionStatus.Closed;
        }

        private CompositeActivity GetRootWorkflow(CompositeActivity activity)
        {
            if (activity.Parent != null)
            {
                CompositeActivity workflow = GetRootWorkflow(activity.Parent);
                return workflow;
            }
            else
            {
                return activity;
            }
        }

        #region Dependency properties

        public static DependencyProperty RuleSetNameProperty = DependencyProperty.Register("RuleSetName", typeof(System.String), typeof(PolicyFromService));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [ValidationOption(ValidationOption.Required)]
        [BrowsableAttribute(true)]
        public string RuleSetName
        {
            get
            {
                return ((String)(base.GetValue(PolicyFromService.RuleSetNameProperty)));
            }
            set
            {
                base.SetValue(PolicyFromService.RuleSetNameProperty, value);
            }
        }       

        #endregion
    }
}
