using System;
using System.Runtime.Serialization;

namespace YuQuan.Models
{
    /// <summary>
    /// 随访记录
    /// </summary>
    [DataContract]
    public class FollowUp
    {
        /// <summary>
        /// 死亡
        /// </summary>
        [DataMember]
        public String Death { get; set; }
        /// <summary>
        /// 心梗
        /// </summary>
        [DataMember]
        public String MI { get; set; }
        /// <summary>
        /// 再血管化
        /// </summary>
        [DataMember]
        public String Revascularization { get; set; }
        /// <summary>
        /// 心衰
        /// </summary>
        [DataMember]
        public String HeartFailure { get; set; }
        /// <summary>
        /// 再次入院
        /// </summary>
        [DataMember]
        public String Readmission { get; set; }
        /// <summary>
        /// 随访化验单
        /// </summary>
        [DataMember]
        public String FollowUpLabSheet { get; set; }
        /// <summary>
        /// 发生事件时间（月）
        /// </summary>
        [DataMember]
        public String CardiacEvent { get; set; }
    }
}
