using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YuQuan.Models
{
    public partial class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public string Gender { get; set; }
        public string PhotoURL { get; set; }
        public string FK_EMR_Patient_Id { get; set; }
    }
}
