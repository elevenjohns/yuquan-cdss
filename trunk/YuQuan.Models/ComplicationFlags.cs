using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class ComplicationFlags
    {
        [DataMember]
        public bool Diabetes { get; set; }
        [DataMember]
        public bool Hypertension { get; set; } 
        [DataMember]
        public bool Hyperlipidemia { get; set; } // 高脂血症
        [DataMember]
        public bool CardiacInsufficiency { get; set; } // 心功能不全
        [DataMember]
        public bool ChronicRenalInsufficiency { get; set; } // 慢性肾功能不全
        [DataMember]
        public bool PeripheralArteryDisease { get; set; } // PAD周围血管病
    }
}
