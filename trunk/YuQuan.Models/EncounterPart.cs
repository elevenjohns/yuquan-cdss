using System.Collections.Generic;
using System;

namespace YuQuan.Models
{
    public partial class Encounter
    {
        public Nullable<System.DateTime> Admission { get; set; }
        public Nullable<System.DateTime> Discharge { get; set; }
        public string Diagnosis { get; set; }

        public string PatientName { get; set; }
        public Nullable<System.DateTime> PatientBirthDay { get; set; }
        public string PatientGender { get; set; }
        public string PatientPhotoURL { get; set; }
        public string PatientId { get; set; }        
        // public Patient Patient { get; set; }

        public string AdmissionDate { get { return Admission.HasValue ? Admission.Value.ToLongDateString() : ""; } }
        public string BedNO { get; set; }
        public string Age { get { return PatientBirthDay.HasValue ? (DateTime.Now.Year - PatientBirthDay.Value.Year).ToString()+"岁" : ""; } }
        public string PatientSummary { get { return PatientName + " " + PatientGender + " " + Age + " " + Diagnosis; } }

        public IList<ClinicalProblemInstance> GetActiveClinicalProblemInstances()
        {
            var list = new List<ClinicalProblemInstance>();

            foreach (var item in this.ClinicalProblemInstance)
            {
                if (item.State == EnumProblemState.New.ToString()||item.State==EnumProblemState.ResolvedSuspected.ToString())
                    list.Add(item);
            }
            return list;
        }

        public IList<ClinicalProblemInstance> GetInactiveClinicalProblemInstances()
        {
            var list = new List<ClinicalProblemInstance>();

            foreach (var item in this.ClinicalProblemInstance)
            {
                if (item.State == EnumProblemState.Resolved.ToString()||item.State==EnumProblemState.Dismissed.ToString())
                    list.Add(item);
            }
            return list;
        }
    }
}
