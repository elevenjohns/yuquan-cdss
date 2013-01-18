using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YuQuan.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GuLangYu.ViewModels
{
    class MainPageViewModelMocker
    {
        private IList<Encounter> encounters;
        public IList<Encounter> Encounters 
        { 
            get
            {
                return encounters;
            }
            set
            {
                if (encounters != value)
                {
                    encounters = value;
                    NotifyPropertyChanged("Encounters");
                }
            }
        }
        private Encounter currentEncounter;
        public Encounter CurrentEncounter {
            get
            {
                return currentEncounter;                
            }
            set
            {
                if (currentEncounter != value)
                {
                    currentEncounter = value;
                    NotifyPropertyChanged("CurrentEncounter");
                }
            }
        }
        public IList<ClinicalProblemInstance> ActiveClinicalProblemInstances { get; set; }

        public IList<ClinicalProblemInstance> InactiveClinicalProblemInstances { get; set; }

        public MainPageViewModelMocker()
        {
            Encounters = new List<Encounter>();

            Encounters.Add(new Encounter()
            {                
                PatientName = "张三",
                PatientGender="男",
                Admission = DateTime.Now,
                Diagnosis = "不稳定性心绞痛",
                PatientPhotoURL = "/GuLangYu;component/Resources/male.PNG",
                BedNO = "01",
                PatientBirthDay = DateTime.Parse("1920/03/24")
            });
            Encounters.Add(new Encounter()
            {
                PatientName = "李四",
                PatientGender = "女",
                Admission = DateTime.Now,
                Diagnosis = "艾滋病;慢性乙肝",
                PatientPhotoURL = "/GuLangYu;component/Resources/female.PNG",
                BedNO="11",
                PatientBirthDay = DateTime.Parse("1950/03/24")
            });
            CurrentEncounter = Encounters[0];


            ActiveClinicalProblemInstances = new List<ClinicalProblemInstance>();

            ActiveClinicalProblemInstances.Add(new ClinicalProblemInstance()
            {
                ClinicalProblemDefinition = new ClinicalProblemDefinition() 
                {
                    Name="高血压",
                    Status="effective"
                },
                State=EnumProblemState.New.ToString()
            });

            ActiveClinicalProblemInstances.Add(new ClinicalProblemInstance()
            {
                ClinicalProblemDefinition = new ClinicalProblemDefinition()
                {
                    Name = "低血压",
                    Status = "effective"
                },
                State = EnumProblemState.New.ToString()
            });

            ActiveClinicalProblemInstances.Add(new ClinicalProblemInstance()
            {
                ClinicalProblemDefinition = new ClinicalProblemDefinition()
                {
                    Name = "高血压",
                    Status = "effective"
                },
                State = EnumProblemState.Resolved.ToString()
            });

            ActiveClinicalProblemInstances.Add(new ClinicalProblemInstance()
            {
                ClinicalProblemDefinition = new ClinicalProblemDefinition()
                {
                    Name = "低血压",
                    Status = "effective"
                },
                State = EnumProblemState.Dismissed.ToString()
            });

            InactiveClinicalProblemInstances = new List<ClinicalProblemInstance>();

            InactiveClinicalProblemInstances.Add(new ClinicalProblemInstance()
            {
                ClinicalProblemDefinition = new ClinicalProblemDefinition()
                {
                    Name = "高血压",
                    Status = "effective"
                },
                State = EnumProblemState.Resolved.ToString()
            });

            InactiveClinicalProblemInstances.Add(new ClinicalProblemInstance()
            {
                ClinicalProblemDefinition = new ClinicalProblemDefinition()
                {
                    Name = "低血压",
                    Status = "effective"
                },
                State = EnumProblemState.Dismissed.ToString()
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
