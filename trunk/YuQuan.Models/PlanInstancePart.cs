using System.Collections.Generic;
using System.Linq;

namespace YuQuan.Models
{
    public partial class PlanInstance
    {
        public PhaseDefinition GetCurrentPhase()
        {
            if(string.IsNullOrEmpty(this.CurrentPhase) == false && this.PlanDefinition!=null && this.PlanDefinition.PhaseDefinition.Count()>0)
                return this.PlanDefinition.PhaseDefinition.FirstOrDefault(x => x.Name == this.CurrentPhase);

            return null;
        }

        public PhaseDefinition SelectedPhase { get; set; }

        public IEnumerable<MedicalOrderInstance> GetMedicalOrderInstance(KBEntities db)
        {
            if (db != null)
            {

                var problemInstance = db.ClinicalProblemInstance.FirstOrDefault(x => x.PlanInstance.Contains(this));

                if (problemInstance != null)
                    return problemInstance.MedicalOrderInstance;
            }

            return new List<MedicalOrderInstance>();            
        }
    }
}
