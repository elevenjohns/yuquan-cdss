using System;
using System.Runtime.Serialization;

namespace YuQuan.Models
{
    /// <summary>
    /// Patient status, context, scenario, paramters
    /// </summary>
    [DataContract]
    public class Scenario
    {
        /// <summary>
        /// whether status is serious
        /// </summary>
        [DataMember]
        bool InSeverity { get; set; }

        #region Habits&LifeStyle
        [DataMember]
        public bool Smooking { get; set; }
        [DataMember]
        public bool Alcoholic { get; set; }
        #endregion

        [DataMember]
        public Anthropometrics Anthropometrics { get; set; }

        [DataMember]
        public PhysiologicalParameters PhysiologicalParameters { get; set; }

        [DataMember]
        public Test Test { get; set; }

        [DataMember]
        public EchoCardiography EchoCardiography { get; set; }

        /// <summary>
        /// Carotid Ultrasound Findings
        /// </summary>
        [DataMember]
        public String CarotidUS { get; set; }

        /// <summary>
        /// Renal Artery Ultrasound Findings
        /// </summary>
        [DataMember]
        public String RenalArteryUS { get; set; }
    }
}
