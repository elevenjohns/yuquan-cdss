//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace YuQuan.Models
{
    public partial class ClinicalProblemInstance
    {
        public ClinicalProblemInstance()
        {
            this.MedicalOrderInstance = new HashSet<MedicalOrderInstance>();
            this.PlanInstance = new HashSet<PlanInstance>();
            this.ChangeRecord = new HashSet<ChangeRecord>();
        }
    
        public int Id { get; set; }
        public string State { get; set; }
        public string Priority { get; set; }
    
        public virtual ClinicalProblemDefinition ClinicalProblemDefinition { get; set; }
        public virtual Encounter Encounter { get; set; }
        public virtual ICollection<MedicalOrderInstance> MedicalOrderInstance { get; set; }
        public virtual ICollection<PlanInstance> PlanInstance { get; set; }
        public virtual ICollection<ChangeRecord> ChangeRecord { get; set; }
        public virtual TriggerRule TriggerRule { get; set; }
    }
    
}