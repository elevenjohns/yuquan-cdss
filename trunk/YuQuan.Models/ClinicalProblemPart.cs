using System.Collections.Generic;
using System.Linq;

namespace YuQuan.Models
{
    public partial class ClinicalProblemDefinition
    {
        /// <summary>
        /// Update ContextItemDefinition Set From TriggerRule when TriggerRule is created or edited
        /// [TODO] unfinished
        /// </summary>
        public void UpdateContextItemFromTriggerRule()
        {
            if (this.TriggerRule != null &&
                string.IsNullOrEmpty(this.TriggerRule.RuleSet) == false)
            {
                // ... Parse the used fact items 
            }
        }

        /// <summary>
        /// Whether this problem has been defined by user
        /// </summary>
        public bool Defined
        {
            get 
            {
                return !(this.TriggerRule == null); 
            }
            private set { }
        }

        /// <summary>
        /// TODO: return the TriggerRule with EBM
        /// </summary>
        public TriggerRule TriggerRuleWithEBM
        {
            get 
            {
                return null;
                //return (this.TriggerRule == null||this.TriggerRule.EBM.Count()<=0)?                    null:this.TriggerRule;
            }
            private set { }
        }

        /// <summary>
        /// TODO: return a list of Plans with EBM
        /// </summary>
        public IEnumerable<PlanDefinition> PlanWithEBM
        {
            get
            {
                var plans = new List<PlanDefinition>();
                //if(this.Intervention!=null && this.Intervention.PlanDefinitionCount()>0)
                //{
                //    foreach (var x in this.Intervention.Plan)
                //    {
                //        if (x.EBM.Count() > 0)
                //            plans.Add(x);
                //    }
                //}
                return plans;
            }
            private set { }
        }

        /// <summary>
        /// TODO: return a list of Orders with EBM
        /// </summary>
        public IEnumerable<MedicalOrderDefinition> OrderWithEBM
        {
            get
            {
                var orders = new List<MedicalOrderDefinition>();
                //if (this.PlanDefinitionCount() > 0)
                //{
                //    foreach (var x in this.Intervention.Plan)
                //    {
                //        foreach (var y in x.Phase)
                //        {
                //            foreach (var z in y.Task)
                //            {
                //                foreach (var o in z.MedicalOrder)
                //                {
                //                    if (o.EBM.Count() > 0)
                //                        orders.Add(o);
                //                }
                //            }
                //        }
                //    }
                //}
                return orders;
            }
            private set { }
        }

        /// <summary>
        /// Avoid using this method for performance
        /// </summary>
        public IEnumerable<ContextItemDefinition> ContextItemDefinition
        {
            get 
            {
                using (var db = new KBEntities())
                {
                    var ContextItemDefintionIds = db.ClinicalProblemDefinition_ContextItemDefinition_AssociationSet.Where(x=>x.ClinicalProblemDefinition_Id == this.Id).Select(y=>y.ContextItemDefinition_Id);
                    var list = new List<ContextItemDefinition>();
                    foreach(var id in ContextItemDefintionIds)
                    {
                        var item = db.ContextItemDefinition.Find(id);
                        if(item != null)
                            list.Add(item);
                    }
                    return list;
                }
            }
        }
    }
}
