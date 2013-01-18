using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class MedicationFlags
    {
        [DataMember]
        public bool Aspirin { get; set; } // 阿司匹林片
        [IgnoreDataMember]
        public bool Clopidogrel { get; set; } // 氯吡格雷片
        [DataMember]
        public bool TirofibanHydrochloride { get; set; } // 盐酸替罗非班，欣维宁
        [DataMember]
        public bool Statins { get; set; } // 他汀
        [DataMember]
        public bool Prolol { get; set; } // 洛尔类, beta blocker 
        [DataMember]
        public bool ACEI_ARB { get; set; }
    }
}
