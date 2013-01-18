using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using YuQuan.Models;
using System.Diagnostics;
using System.IO;

namespace WuKeSong.Workflows
{
    public sealed class PathwayRecommendCodeActivity : CodeActivity
    {
        public InArgument<ClinicalPathwayInstance> Instance { get; set; }
        public OutArgument<string> RecommendedPathway { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            var instance = context.GetValue(this.Instance);
            context.SetValue(this.RecommendedPathway, instance.Diagnosis);

            //// log
            //using (StreamWriter sw = File.AppendText(@"c:\workflow.log"))
            //{
            //    sw.WriteLine(DateTime.Now.ToString() + ": " + context.GetValue(this.RecommendedPathway));
            //}
        }
    }
}
