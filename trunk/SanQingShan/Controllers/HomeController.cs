using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using SanQingShan.Models;
using YuQuan.Helpers;
using System.Data.Entity;
using SanQingShan.Helpers;
using YuQuan.Untility;

namespace SanQingShan.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CPEntities db = new CPEntities();

        public ActionResult Index()
        {
            db.SaveChanges();

            ViewBag.TotalCase = db.CP_VISIT.Count();            
            return View(db.CP_VISIT);
        }

        public ActionResult Search(String keyword)
        {
            String searchString = keyword;
            var instance = from item in db.CP_VISIT select item; //create LINQ query
            if (!String.IsNullOrEmpty(searchString))
                instance = instance.Where(s => (
                    s.CP_ID.Contains(searchString) ||
                    s.DIAGNOSIS.Contains(searchString))); // s.OUTPATIENT_ID.Contains(searchString)
            return View(instance);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            ViewBag.DbContext = db;
            var instance = db.CP_VISIT.First(x => x.Id == id);
            return View(instance);
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        private ActionResult Import(int start=0)
        {
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // Seed VISIT records
                OracleDbHelper.SeedVisitTable(db);

                // Use ExcelImporter to import CP_ORDER table.
                var excelImporter = new ExcelImporter(db);
                excelImporter.Import(start);

                // Use OracleDbHelper to import all other tables.
                OracleDbHelper.PopulateDB<CP_VITAL_SIGN>(db);
                OracleDbHelper.PopulateDB<CP_EXAM>(db);
                OracleDbHelper.PopulateDB<CP_LAB_TEST>(db);
                OracleDbHelper.PopulateDB<CP_MEDICAL_DOC>(db); // alone cost: 3.23801252166667 minutes. 

                //return ("Total cost: " + sw.Elapsed.TotalMinutes + "minutes. ");
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction("Error",new{message = e.Message });
            }
        }
    }
}
