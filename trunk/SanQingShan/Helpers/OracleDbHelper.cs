using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SanQingShan.Models;
using YuQuan.Helpers;
using System.Data.OracleClient;
using System.Data;
using System.Reflection;
using System.Data.Entity;
using System.Diagnostics;
using YuQuan.Untility;

namespace SanQingShan.Helpers
{
    public static class OracleDbHelper
    {
        public static void PopulateDB<T>(DbContext db) where T : class
        {
            var sw = Stopwatch.StartNew();
            if (db == null)
                throw new ArgumentNullException("DbContext is null");

            Type[] validTypes = { typeof(CP_EXAM), typeof(CP_LAB_TEST), typeof(CP_MEDICAL_DOC), typeof(CP_ORDER), typeof(CP_VITAL_SIGN) };
            if (validTypes.Contains(typeof(T)) == false)
                throw new NotSupportedException("Type is not supported");

            var dbSet = DbContextHelper.GetDbSet<T>(db);
            if (dbSet == null)
                throw new NotSupportedException("DbContext doesnot have corrsponding DbSet");

            string connectionString = "data source=CPCB;user id=system;password=Y4QHID";
            string selectQuery = "select * from " + typeof(T).Name + " t";
            var conn = new OracleConnection(connectionString);
            var comm = new OracleCommand(selectQuery, conn);
            conn.Open();
            var reader = comm.ExecuteReader(CommandBehavior.CloseConnection);

            Object[] values = new Object[reader.FieldCount];
            int fieldCount = reader.FieldCount;
            while (reader.Read())
            {
                Type[] constrParam = new Type[0];
                var obj = typeof(T).GetConstructor(constrParam).Invoke(constrParam);
                reader.GetValues(values);

                for (int i = 0; i < fieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var value = values[i];
                    foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
                    {
                        if (propertyInfo.Name == name)
                        {
                            if (propertyInfo.CanWrite)
                            {
                                if (value is System.DBNull || value == null)
                                {
                                    // do nothing for null field
                                }
                                else
                                {
                                    propertyInfo.SetValue(obj, value, null);
                                }
                            }
                        }
                    }
                }
                ((DbSet<T>)dbSet).Add((T)obj);
                db.SaveChanges();
            }

            reader.Close();
            //Implicitly closes the connection because CommandBehavior.CloseConnection was specified.

            sw.Stop();
            SimpleLog.WriteLog(typeof(T).Name + " consumes: "+ sw.Elapsed.TotalMinutes + " minutes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="start">row NO</param>
        public static void SeedVisitTable(DbContext db, int start = 0)
        {
            if (db as CPEntities == null)
                return;

            var ds = new DataSet();
            ds.ReadXml(@"c:\visit.xml"); // visit.xml is exported from Oracle CP_Pat301 table, which contains visit info
            var dt = ds.Tables[0];

            for (int i = start; i < dt.Rows.Count; i++)
            {
                DateTime dtStart = new DateTime();
                bool flag_Start = DateTime.TryParse(dt.Rows[i]["ADMISSION_DATE_TIME"].ToString(), out dtStart);
                DateTime dtEnd = new DateTime();
                bool flag_End = DateTime.TryParse(dt.Rows[i]["DISCHARGE_DATE_TIME"].ToString(), out dtEnd);
                DateTime dtDiag = new DateTime();
                bool flag_Diag = DateTime.TryParse(dt.Rows[i]["DIAGNOSIS_DATE"].ToString(), out dtDiag);
                DateTime dtBirth = new DateTime();
                bool flag_Birth = DateTime.TryParse(dt.Rows[i]["DATE_OF_BIRTH"].ToString(), out dtBirth);

                var visit = new CP_VISIT()
                {
                    HOSPITAL_ID = "00000001",
                    //ADMISSION_TIME = DateTime.Parse(dt.Rows[i]["ADMISSION_DATE_TIME"] as string),
                    //DISCHARGE_TIME = DateTime.Parse(dt.Rows[i]["DISCHARGE_DATE_TIME"] as string),
                    //DIAGNOSIS_TIME = DateTime.Parse(dt.Rows[i]["DIAGNOSIS_DATE"] as string),
                    //BIRTHDAY = DateTime.Parse(dt.Rows[i]["DATE_OF_BIRTH"] as string),
                    CONDITION = dt.Rows[i]["PAT_CONDITION_NAME"] as string,
                    CP_ID = dt.Rows[i]["CP_ID"] as string,
                    DEPARTMENT = dt.Rows[i]["DEPT_NAME"] as string,
                    DIAGNOSIS = dt.Rows[i]["DIAGNOSIS_DESC"] as string,
                    DOCTOR = dt.Rows[i]["DIRECTOR"] as string,
                    GENDER = dt.Rows[i]["SEX"] as string,
                    INPATIENT_NO = dt.Rows[i]["INPATIENT_NO"] as string,
                    OCCUPATION = dt.Rows[i]["OCCUPATION_NAME"] as string,
                    OUTPATIENT_ID = dt.Rows[i]["OUTPATIENT_ID"] as string,
                    TOTAL_COSTS = dt.Rows[i]["TOTAL_COSTS"] as string,
                    TOTAL_PAYMENTS = dt.Rows[i]["TOTAL_PAYMENTS"] as string,
                    VISIT_NO = dt.Rows[i]["VISIT_ID"] as string
                };

                if (flag_Start) visit.ADMISSION_TIME = dtStart;
                if (flag_End) visit.DISCHARGE_TIME = dtEnd;
                if (flag_Diag) visit.DIAGNOSIS_TIME = dtDiag;
                if (flag_Birth) visit.BIRTHDAY = dtBirth;

                (db as CPEntities).CP_VISIT.Add(visit);
                db.SaveChanges();
            }
        }
    }
}