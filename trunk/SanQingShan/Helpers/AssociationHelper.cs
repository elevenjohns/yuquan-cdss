using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SanQingShan.Models;

namespace SanQingShan.Helpers
{
    /// <summary>
    /// Get assiated items for a specific case
    /// </summary>
    /// <remarks>I built index in db(Refer to index.sql). Use LINQ2SQL command to get acceptable performance</remarks>
    public class AssociationHelper
    {
        public static IEnumerable<CP_ORDER> GetOrders(string CP_ID, CPEntities db)
        {
            return db == null ? null : db.CP_ORDER.Where(x => x.CP_ID == CP_ID);
        }
        public static IEnumerable<CP_LAB_TEST> GetLabTests(string CP_ID, CPEntities db)
        {
            return db == null ? null : db.CP_LAB_TEST.Where(x => x.CP_ID == CP_ID);
        }
        public static IEnumerable<CP_VITAL_SIGN> GetVitalSigns(string CP_ID, CPEntities db)
        {
            return db == null ? null : db.CP_VITAL_SIGN.Where(x => x.CP_ID == CP_ID);
        }
        public static IEnumerable<CP_MEDICAL_DOC> GetMedicalDocs(string CP_ID, CPEntities db)
        {
            return db == null ? null : db.CP_MEDICAL_DOC.Where(x => x.CP_ID == CP_ID);
        }
        public static IEnumerable<CP_EXAM> GetExams(string CP_ID, CPEntities db)
        {
            return db == null ? null : db.CP_EXAM.Where(x => x.CP_ID == CP_ID);
        }
    }
}