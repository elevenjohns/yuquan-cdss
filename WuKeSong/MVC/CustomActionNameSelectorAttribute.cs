using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace WuKeSong.MVC
{
    /// <summary>
    /// An Action selector attribute that selects the action based on the 'trigger' name
    /// (which matches the 'name' property of the hitted web form submit button).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class CustomActionNameSelectorAttribute : ActionNameSelectorAttribute
    {
        /// <summary>
        /// Instantiates a new CustomActionNameSelectorAttribute instance.
        /// </summary>
        /// <param name="requestVariableName">
        /// Name of the variable matching the name of the submit trigger.
        /// </param>
        public CustomActionNameSelectorAttribute(string requestVariableName)
        {
            this.RequestVariableNames = new string[] { requestVariableName };
        }

        /// <summary>
        /// Instantiates a new CustomActionNameSelectorAttribute instance.
        /// </summary>
        /// <param name="requestVariableNames">
        /// Name of the variable(s) matching the name of the submit trigger.
        /// </param>
        public CustomActionNameSelectorAttribute(params string[] requestVariableNames)
        {
            this.RequestVariableNames = requestVariableNames;
        }

        /// <summary>
        /// Name of the variable(s) matching the name of the submit trigger.
        /// </summary>
        public string[] RequestVariableNames { get; set; }

        /// <summary>
        /// Whether the given action matches this selector.
        /// </summary>
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            foreach (string requestVariableName in RequestVariableNames)
            {
                if (controllerContext.HttpContext.Request[requestVariableName] != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}