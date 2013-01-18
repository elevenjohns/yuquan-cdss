using System.Runtime.Serialization;

namespace YuQuan.Models
{
    /// <summary>
    /// 人体测量
    /// </summary>
    [DataContract]
    public class Anthropometrics
    {
        /// <summary>
        /// in cm
        /// </summary>
        [DataMember]
        public int BodyHeight { get; set; } 
        /// <summary>
        /// in kg
        /// </summary>
        [DataMember]
        public int BodyWeight { get; set; }
        /// <summary>
        /// Body Mass Index
        /// </summary>
        [DataMember]
        public float BMI { get; set; }
        /// <summary>
        /// waist circumference
        /// </summary>
        [DataMember]
        public float WC { get; set; }
        /// <summary>
        /// hip circumference
        /// </summary>
        [DataMember]
        public float HC { get; set; }
    }
}
