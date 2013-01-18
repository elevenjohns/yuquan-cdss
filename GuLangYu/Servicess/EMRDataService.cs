using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuLangYu.Interfaces;
using YuQuan.Models;
using SanQingShan.Models;
using YuQuan.Helpers;

namespace GuLangYu.Servicess
{
    public class EMRDataService : IEMRDataService
    {

        #region IEMRDataService Members

        public IList<YuQuan.Models.Encounter> GetActiveEncounterList(KBEntities db)
        {
            var db2 = new CPEntities();
            var list = new List<Encounter>();

            if (db == null || db.Encounter == null)
                return list;

            foreach (var x in db.Encounter)
            {                
                var y = db2.CP_VISIT.FirstOrDefault(z => z.CP_ID == x.FK_EMR_Encounter_Id);
                if (y == null) continue;
                //var encounter = new Encounter()
                //{
                //    PatientId = y.INPATIENT_NO,
                //    PatientName = ChineseNameGenerator.GetChineseName(),
                //    Diagnosis = y.DIAGNOSIS,
                //    Admission = y.ADMISSION_TIME,
                //    Discharge = y.DISCHARGE_TIME,
                //    PatientBirthDay = y.BIRTHDAY,
                //    PatientGender = y.GENDER,
                //    PatientPhotoURL = y.GENDER == "女" ?
                //    "/GuLangYu;component/Resources/female.PNG" : 
                //    "/GuLangYu;component/Resources/male.PNG",
                //    BedNO = x.Id.ToString(),
                //    FK_EMR_Encounter_Id = x.FK_EMR_Encounter_Id
                //};
                x.PatientId = y.INPATIENT_NO;
                x.PatientName = ChineseNameGenerator.GetChineseName();
                x.Diagnosis = y.DIAGNOSIS;
                x.Admission = y.ADMISSION_TIME;
                x.Discharge = y.DISCHARGE_TIME;
                x.PatientBirthDay = y.BIRTHDAY;
                x.PatientGender = y.GENDER;
                x.PatientPhotoURL = (y.GENDER == "女") ? "/GuLangYu;component/Resources/female.PNG" : "/GuLangYu;component/Resources/male.PNG";
                x.BedNO = x.Id.ToString();
                x.FK_EMR_Encounter_Id = x.FK_EMR_Encounter_Id;
                
                list.Add(x); // use original entity, not copied entity. Because later we need to access its navigation property
            }
            return list;
        }

        public IList<YuQuan.Models.Encounter> GetEncounterList(string PatientId = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
