//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace YuQuan.Models
{
    public partial class ClinicalProblemDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReferenceURL { get; set; }
        public string Code { get; set; }
        public string CodingSystem { get; set; }
        public Nullable<bool> Infectious { get; set; }
        public string InputCode { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    
        public virtual TriggerRule TriggerRule { get; set; }
    }
    
}
