using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace YuQuan.Models
{
    public partial class ClinicalProblemInstance
    {
        public ICommand SolveCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand ReviveCommand { get; set; }  

        /// <summary>
        /// Get facts of a specific state change for the problem instance.
        /// </summary>
        public IEnumerable<Fact> GetFacts(string state)
        {
            var changeRecord = this.ChangeRecord.LastOrDefault(x=>x.NewState == state);
            if(changeRecord == null)
                return new List<Fact>();

            return changeRecord.Fact.ToList();
        }

        /// <summary>
        /// Get facts for current state of the problem instance.
        /// </summary>
        public IEnumerable<Fact> Facts
        {
            get
            {
                return GetFacts(this.State);
            }
        }

        /// <summary>
        /// Get fact series from historical state change records.
        /// </summary>
        public IDictionary<DateTime,Fact> GetFactSeries(string fact)
        {
            var series = new Dictionary<DateTime, Fact>();
            if (this.Facts.Any(x => x.ContextItemDefinition.Name == fact) == false)
                return series;

            foreach (var x in this.ChangeRecord)
            {
                foreach (var y in x.Fact)
                {
                    if (y.ContextItemDefinition.Name == fact)
                        series.Add(x.TimeStamp.Value, y);
                }
            }
            return series;
        }

        /// <summary>
        /// Get evidences for the problem instance.
        /// Evidences are inferred from member facts.
        /// </summary>
        public IEnumerable<Report> Reports { 
            get
            {
                var reports = new List<Report>();
                this.Facts.ToList().ForEach(x=>
                    {
                        if(reports.Contains(x.Report,new IdComparer()) == false)
                            reports.Add(x.Report);
                    });
                return reports;
            }
        }

        public string PPV
        {
            get 
            {
                if (this.ClinicalProblemDefinition != null && this.ClinicalProblemDefinition.TriggerRule != null && this.ClinicalProblemDefinition.TriggerRule.PPV != null)
                    return this.ClinicalProblemDefinition.TriggerRule.PPV;
                return EnumPPVLevel.Doubtless.ToString(); // means the problem is manually created. PPV = 100%
            }
        }

        public DateTime CreationTime
        {
            get 
            {
                if (this.ChangeRecord != null && this.ChangeRecord.Count() > 0)
                {
                    if(this.ChangeRecord.ElementAt(0).TimeStamp.HasValue)
                        return this.ChangeRecord.ElementAt(0).TimeStamp.Value;
                }
                return DateTime.Now; // In real application, this should not happen.
            }
        }

        private class IdComparer : IEqualityComparer<Report>
        {
            #region IEqualityComparer<Report> Members

            bool IEqualityComparer<Report>.Equals(Report x, Report y)
            {
                return x.Id == y.Id;
            }

            int IEqualityComparer<Report>.GetHashCode(Report obj)
            {
                return obj.Id.GetHashCode();
            }

            #endregion
        }
    }
}
