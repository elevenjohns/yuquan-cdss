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
    public partial class Report
    {
        public Report()
        {
            this.Fact = new HashSet<Fact>();
        }
    
        public int Id { get; set; }
        public string URL { get; set; }
        public string ReportType { get; set; }
        public System.DateTime TimeStamp { get; set; }
    
        public virtual ICollection<Fact> Fact { get; set; }
        public virtual Event Event { get; set; }
    }
    
}