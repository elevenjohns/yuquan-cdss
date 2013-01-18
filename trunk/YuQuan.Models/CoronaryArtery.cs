using System;
using System.Runtime.Serialization;

namespace YuQuan.Models
{
    /// <summary>
    /// Stenosis Level:
    /// 0:0-49%
    /// 1:50-74%
    /// 2：75-89%
    /// 3:90-99%
    /// 4:100% 
    /// </summary>
    [DataContract]
    public enum StenosisLevel
    {
        Level_0,
        Level_1,
        Level_2,
        Level_3,
        Level_4
    }

    [DataContract]
    public struct CoronaryArterySegments
    {
        bool Proximal;
        bool Middle;
        bool Remote;
        bool Opening;
    }

    [DataContract]
    public class CoronaryArtery
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public StenosisLevel StenosisLevel { get; set; }
        [DataMember]
        public CoronaryArterySegments StenosisSegments { get; set; }
        [DataMember]
        public String StenosisSegmentsRaw { get; set; }
        /// <summary>
        /// PCI治疗次数，e.g. 支架/搭桥个数
        /// </summary>
        [DataMember]
        public int PCI { get; set; }
    }
}
