using System;
using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class CodedTerm
    {
        [DataMember]
        public String Code { get; set; }
        [DataMember]
        public String CodingSystem { get; set; }
        /// <summary>
        /// may contain modifiers, indicating certainty
        /// </summary>
        [DataMember]
        public String Literal { get; set; }
        /// <summary>
        /// Concept Unique ID
        /// </summary>
        [DataMember]
        public String CUI { get; set; }
    }
}
