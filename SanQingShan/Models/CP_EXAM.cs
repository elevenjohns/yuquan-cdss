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
    public partial class CP_EXAM
    {
        public int Id { get; set; }
        public string HOSPITAL_ID { get; set; }
        public string OUTPATIENT_ID { get; set; }
        public string CP_ID { get; set; }
        public string EXAM_CLASS { get; set; }
        public string EXAM_SUB_CLASS { get; set; }
        public Nullable<System.DateTime> EXAM_TIME { get; set; }
        public string RESULT_PARAGRAPH { get; set; }
        public string RESULT_DESCRIPTION { get; set; }
        public string RESULT_IMPRESSION { get; set; }
        public string IS_ABNORMAL { get; set; }
        public string ORDER_ID { get; set; }
    }
    
}
