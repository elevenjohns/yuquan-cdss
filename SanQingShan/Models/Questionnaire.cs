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

namespace SanQingShan.Models
{
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            this.Question = new HashSet<Question>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    
        public virtual ICollection<Question> Question { get; set; }
    }
    
}
