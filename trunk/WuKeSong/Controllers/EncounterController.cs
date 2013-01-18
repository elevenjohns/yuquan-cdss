using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SanQingShan.Models;

namespace WuKeSong.Controllers
{
    public class EncounterController : Controller
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">CP ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            ViewBag.DbContext = db;
            var instance = db.CP_VISIT.FirstOrDefault(x => x.CP_ID == id);
            if(instance == null)
                return RedirectToAction("Error", new {message="No Information for this item"});
            return View(instance);
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }        
    }
}
