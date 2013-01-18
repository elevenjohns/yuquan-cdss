using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SanQingShan.Models
{
    public partial class CP_MEDICAL_DOC
    {
        public string FilePath { get { return ConfigurationManager.AppSettings["DocumentRetrievalProtocol"]+this.FULL_PATH; } }
    }
}