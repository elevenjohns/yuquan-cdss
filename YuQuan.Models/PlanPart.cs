using System.Linq;

namespace YuQuan.Models
{
    public partial class PlanDefinition
    {
        public string Type
        {
            get 
            {
                // There is a hidden contract: a plan with only one phase that has the same name as plan itselt, is an order set.
                // Otherwise, it is a pathway
                if ((this.PhaseDefinition.Count() == 1 && this.PhaseDefinition.ElementAt(0).Name == this.Name) || this.PhaseDefinition.Count() == 0)
                {
                    return "医嘱集";
                }
                else
                {
                    return "临床路径";
                }
            }
            private set { }
        }
    }
}
