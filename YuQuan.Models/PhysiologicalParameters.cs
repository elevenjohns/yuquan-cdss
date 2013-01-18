using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class PhysiologicalParameters
    {
        [DataMember]
        public int BodyTemperature { get; set; }
        /// <summary>
        /// Heart Rate
        /// </summary>
        [DataMember]
        public int HR { get; set; }
        /// <summary>
        /// Systolic blood pressure
        /// </summary>
        [DataMember]
        public int BpSys { get; set; }
        /// <summary>
        /// Diastolic blood pressure
        /// </summary>
        [DataMember]
        public int BpDia { get; set; } 
    }
}
