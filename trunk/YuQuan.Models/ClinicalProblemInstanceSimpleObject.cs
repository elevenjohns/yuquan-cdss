using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YuQuan.Models
{
    /// <summary>
    /// A simple object for clinical problem instance, that is intended to be transmitted over network. i.e. used as a parameter in Web Service
    /// </summary>
    [DataContract]
    public class ClinicalProblemInstanceSimpleObject
    {
        [DataMember]
        public int Id { get;set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        public List<string> Facts { get; set; }
        [DataMember]
        public List<KeyValuePair<string, string>> Reports { get; set; } // name, and url
        [DataMember]
        public string TriggerRule { get; set; } // click to navigate to corresponding problem definition
        [DataMember]
        public List<string> History { get; set; }
        [DataMember]
        public List<string> OrderSet { get; set; } // [TODO] how to integrate pathway needs further design
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public string State { get; set; }

        public string DateStamp 
        {
            get { return TimeStamp.ToLongDateString(); }
        }
    }
}
