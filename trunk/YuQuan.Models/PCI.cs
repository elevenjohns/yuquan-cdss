using System;
using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class PCI
    {
        [DataMember]
        public DateTime SurgeryDay { get; set; } // 手术日期
        [DataMember]
        public bool Emergency_PCI { get; set; } // 急诊PCI
        [DataMember]
        public CoronaryAngiography CA { get; set; } // 冠脉造影所见及治疗
    }
}
