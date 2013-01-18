using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YuQuan.Models;
using System.ComponentModel;
using GuLangYu.Properties;
using GuLangYu.Interfaces;
using GuLangYu.Servicess;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using WuKeSong.Services;
using WuKeSong.Interfaces;

namespace GuLangYu.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private KBEntities db = new KBEntities();

        private IEMRDataService dataService;
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

                    NotifyPropertyChanged("ActiveClinicalProblemInstances");
                    NotifyPropertyChanged("InactiveClinicalProblemInstances");
                    NotifyPropertyChanged("PatientSummary");

                    if (currentEncounter == null)
                        URL = ConfigurationManager.AppSettings["HomePage"];
                    else
                        URL = ConfigurationManager.AppSettings["EncounterPage"]+currentEncounter.FK_EMR_Encounter_Id;
                }
            }
        }
        private IDecisionSupportService decisionSupportService;

        public IList<ClinicalProblemInstance> ActiveClinicalProblemInstances
        {
            get { return currentEncounter == null ? null : currentEncounter.GetActiveClinicalProblemInstances(); }            
        }

        public IList<ClinicalProblemInstance> InactiveClinicalProblemInstances
        {
            get { return currentEncounter == null ? null : currentEncounter.GetInactiveClinicalProblemInstances(); }
        }

        public string PatientSummary { get { return CurrentEncounter==null? "":CurrentEncounter.PatientSummary; } }
        
        private string url;
        public string URL { 
            get { return url; }
            set 
            {
                if (value != url)
                {
                    url = value;
                    NotifyPropertyChanged("URL");
                }
            }
        }

        public ICommand AddProblemCommand { get; set; }      

        public MainPageViewModel()
        {
            dataService = new EMRDataService();
            Encounters = dataService.GetActiveEncounterList(db);

            decisionSupportService = new DecisionSupportService();
            RegisterCommand();

            CurrentEncounter = null;
            this.AddProblemCommand = new DelegateCommand(() => 
            {
                if (CurrentEncounter != null)
                {
                    // pop up add problem dialog
                    //
                    // AddProblem(CurrentEncounter.Id,);
                }
            });
        }

        private void RegisterCommand()
        {
            foreach (var x in Encounters)
            {
                foreach (var y in x.ClinicalProblemInstance)
                {
                    y.SolveCommand = new DelegateCommand(()=>
                    {
                        UpdateProblemState(y.Id, EnumStateTransition.Solve.ToString());
                    });
                    y.DismissCommand = new DelegateCommand(() =>
                    {
                        UpdateProblemState(y.Id, EnumStateTransition.Dismiss.ToString());
                    });
                    y.ReviveCommand = new DelegateCommand(() =>
                    {
                        UpdateProblemState(y.Id, EnumStateTransition.Revive.ToString());
                    });
                }
            }
        }

        private void UpdateProblemState(int id,string operation)
        {
            decisionSupportService.UpdateProblemState(id, operation);
            NotifyPropertyChanged("ActiveClinicalProblemInstances");
            NotifyPropertyChanged("InactiveClinicalProblemInstances");
        }

        private void AddProblem(int encounter_id, int problem_definition_id)
        {
            decisionSupportService.AddProblem(encounter_id, problem_definition_id);
            NotifyPropertyChanged("ActiveClinicalProblemInstances");
        }

        ~MainPageViewModel()
        {
            this.db.Dispose();
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
