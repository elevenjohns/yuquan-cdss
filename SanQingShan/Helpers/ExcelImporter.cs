using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuQuan.Untility;
using System.IO;
using System.Data;
using SanQingShan.Models;
using System.Diagnostics;

namespace SanQingShan.Helpers
{
    public class ExcelImporter
    {
        private const int _digits = 8; // CPID has 8 digits
        private ExcelOperator excel = null;
        private CPEntities db = null;
        private IDictionary<string, string> caseDict;
        private IList<string> orderHeaders;

        public ExcelImporter(CPEntities db)
        {
            this.db = db;
            //[NOTE] Requires the data folder located in D:\301\export
            this.caseDict = DictHelper.GetCaseDict();
            this.orderHeaders = DictHelper.GetOrderHeaders();
        }

        /// <summary>
        /// Pad empty digits with 0
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private String GetIDString(int id)
        {
            String str_IDLine = id.ToString();
            int IDDigit = id.ToString().Length;
            for (int i = 0; i < _digits - IDDigit; i++)
            {
                str_IDLine = "0" + str_IDLine;
            }
            return str_IDLine;
        }

        private void PreProcess(int CPID)
        {
            IList<int> ColumnsToBeRemoved = new List<int>();
            ColumnsToBeRemoved.Clear();
            //tailor
            String strRemove = "1,1,1,1,6,6,9,9,10";
            char[] spliterRemove = { ',' };
            string[] strResult = strRemove.Split(spliterRemove);
            int strCount = strResult.Count();
            for (int i = 0; i < strCount; i++)
            {
                int temp;
                bool flag = Int32.TryParse(strResult[i], out temp);
                if (flag)
                    ColumnsToBeRemoved.Add(temp);
            }

            //删除无用列
            int countOfColumns = ColumnsToBeRemoved.Count;
            if (countOfColumns > 0)
                for (int i = 0; i < countOfColumns; i++)
                    excel.DataSetFromFile.Tables[0].Columns.RemoveAt(ColumnsToBeRemoved[i]);

            // Update Column Names
            // For some data, last column is empty. In this case, column number is less.
            for (int i = 0; i < Math.Min(this.orderHeaders.Count,excel.DataSetFromFile.Tables[0].Columns.Count); i++)
                excel.DataSetFromFile.Tables[0].Columns[i].ColumnName = orderHeaders[i];

            //添加CPID列       
            var dc_CPID = new DataColumn();
            dc_CPID.ColumnName = "CPID";
            dc_CPID.DataType = typeof(System.String);
            excel.DataSetFromFile.Tables[0].Columns.Add(dc_CPID);
            dc_CPID.SetOrdinal(0);

            //添加HospitalID列
            DataColumn dc_HospitalID = new DataColumn();
            dc_HospitalID.ColumnName = "HospitalID";
            dc_HospitalID.DataType = typeof(System.String);
            excel.DataSetFromFile.Tables[0].Columns.Add(dc_HospitalID);
            dc_HospitalID.SetOrdinal(0);

            //添加OrderID列
            DataColumn dc_OrderID = new DataColumn();
            dc_OrderID.ColumnName = "OrderID";
            dc_OrderID.DataType = typeof(System.String);
            excel.DataSetFromFile.Tables[0].Columns.Add(dc_OrderID);
            dc_OrderID.SetOrdinal(0);

            int excelRowCount = excel.DataSetFromFile.Tables[0].Rows.Count;
            for (int i = 0; i < excelRowCount; i++)
            {
                //CPID
                excel.DataSetFromFile.Tables[0].Rows[i]["CPID"] = GetIDString(CPID);

                //HospitalID
                excel.DataSetFromFile.Tables[0].Rows[i]["HospitalID"] = "00000001";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startCase">case NO</param>
        public void Import(int startCase = 4)
        {
            if (db == null)
                throw new ArgumentNullException("DbContext is null");

            //循环处理文件夹, CPID starts from 4
            // 18 is the 1st abnormal excel with less columns
            // row 384 is abnormal
            for (int CPID = startCase; CPID < caseDict.Count + 4; CPID++)
            {
                try
                {
                    String currentPath;
                    caseDict.TryGetValue(GetIDString(CPID), out currentPath);
                    if (File.Exists(currentPath))
                    {
                        excel = new ExcelOperator(currentPath, 1, false);
                        // min column should be more exact, here we use 10
                        if (excel.DataSetFromFile.Tables[0].Columns.Count < 10 || excel.DataSetFromFile.Tables[0].Rows.Count < 1)
                            continue;
                    }
                    else
                    {
                        continue;
                    }

                    PreProcess(CPID);

                    var dt = excel.DataSetFromFile.Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime dtStart = new DateTime();
                        bool flag_Start = DateTime.TryParse(dt.Rows[i]["StartTime"].ToString(), out dtStart);
                        DateTime dtEnd = new DateTime();
                        bool flag_End = DateTime.TryParse(dt.Rows[i]["EndTime"].ToString(), out dtEnd);
                        DateTime dtReal = new DateTime();
                        bool flag_Real = (dt.Columns.Contains("RealExecutiveTime")) && DateTime.TryParse(dt.Rows[i]["RealExecutiveTime"].ToString(), out dtReal);

                        //System.Data.DataTable dtCount =conn ("select count(*) from cp_orders1 t where t.cp_id ="+"'"+GetID(n_CPID)+"'");

                        var order = new CP_ORDER()
                        {
                            //CHECKER = dt.Rows[i]["Checker"] as string,
                            CP_ID = dt.Rows[i]["CPID"] as string,
                            DOSAGE = dt.Rows[i]["Dosage"] as string,
                            //END_TIME = DateTime.Parse(dt.Rows[i]["EndTime"] as string),
                            //EXECUTOR = dt.Rows[i]["Executor"] as string,
                            EXPECTED_EXECUTIVE_TIME = (dt.Rows[i]["ExpectedTime"] as string).Replace("'", ""),
                            FREQ = (dt.Rows[i]["Freq"] as string).ToString().Replace("'", ""),
                            FREQ_UNIT = dt.Rows[i]["FreqUnit"] as string,
                            HOSPITAL_ID = dt.Rows[i]["HospitalID"] as string,
                            ISSUER = dt.Rows[i]["Issuer"] as string,
                            METHOD = dt.Rows[i]["Method"] as string,
                            ORDER_CONTENT = dt.Rows[i]["Content"] as string,
                            ORDER_TYPE_ID = dt.Rows[i]["Type"] as string,
                            OUTPATIENT_ID = dt.Rows[i]["OutpatientID"] as string,
                            //REAL_EXECUTIVE_TIME = DateTime.Parse(dt.Rows[i]["RealExecutiveTime"] as string),
                            //START_TIME = DateTime.Parse(dt.Rows[i]["StartTime"] as string),
                            //TERMINATOR = dt.Rows[i]["Teminator"] as string,
                            UNIT = dt.Rows[i]["Unit"] as string
                        };

                        // For last few columns, check whether they exist
                        if (dt.Columns.Contains("Teminator"))
                            order.TERMINATOR = dt.Rows[i]["Teminator"] as string;
                        if (dt.Columns.Contains("Executor"))
                            order.EXECUTOR = dt.Rows[i]["Executor"] as string;
                        if (dt.Columns.Contains("Checker"))
                            order.CHECKER = dt.Rows[i]["Checker"] as string;
                        // 
                        // Exception: of a datetime2 data type to a datetime data type resulted in an out-of-range value
                        // We found in excel files, some datetime are obviously wrong, e.g. case 384 excel's line 120: 1508-6-6 15:06:00
                        // DATETIME supports 1753/1/1 to "eternity" (9999/12/31), while DATETIME2 support 0001/1/1 through eternity.
                        // To fix it, judge datetime range.
                        if (flag_Real)
                        {
                            order.REAL_EXECUTIVE_TIME = DateTime.Parse(dt.Rows[i]["RealExecutiveTime"] as string);
                            if (order.REAL_EXECUTIVE_TIME < new DateTime(1753, 1, 1))
                                order.REAL_EXECUTIVE_TIME = null;
                                
                        }

                        if (flag_End)
                        {
                            order.END_TIME = DateTime.Parse(dt.Rows[i]["EndTime"] as string);
                            if (order.END_TIME < new DateTime(1753, 1, 1))
                                order.END_TIME = null;
                        }
                        if (flag_Start)
                        {
                            order.START_TIME = DateTime.Parse(dt.Rows[i]["StartTime"] as string);
                            if (order.START_TIME < new DateTime(1753, 1, 1))
                                order.START_TIME = null;
                        }

                        db.CP_ORDER.Add(order);
                    }
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Case "+CPID+" failed");
                    Debug.WriteLine(ex.Message);
                    SimpleLog.WriteLog("Case " + CPID + " failed. " + ex.Message); 
                }
            }
        }
    }
}