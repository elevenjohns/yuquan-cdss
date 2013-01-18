using System.Web.Mvc;
using System.Diagnostics;
using WuKeSong.Data;
using System;
using YuQuan.Models;
using System.Collections.Generic;
using System.Linq;
using WuKeSong.Services;

namespace WuKeSong.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private KBEntities db = new KBEntities();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Seed database by sample data
        /// </summary>
        /// <returns></returns>
        public ActionResult Seed()
        {
            string response;
            string performance;

            var sw = Stopwatch.StartNew();
            try
            {
                (new KBEntitiesInitializer()).Seed();
                response = "Seed success!" + System.Environment.NewLine + "For rule set, can also use ExternalRuleSetTool";
            }
            catch (Exception ex)
            {
                response = "Seed failed!" + System.Environment.NewLine + " Error message:" + ex.Message;
            }
            finally
            {
                sw.Stop();
                performance = "Total consumed time (ms): " + sw.ElapsedMilliseconds;
            }

            ViewBag.Response = response;
            ViewBag.Performance = performance;
            return View();
        }


        // Test Web Services
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(FormCollection collection)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var bpSys = collection["BpSys"];
            var bpDia = collection["BpDia"];
            var cTnT = collection["CTnT"];
            var cTnI = collection["CTnI"];

            var context = new Context();
            context.NumericParameters.Add("肌钙蛋白T测定", double.Parse(cTnT));
            context.NumericParameters.Add("肌钙蛋白I测定", double.Parse(cTnI));
            context.NumericParameters.Add("肱动脉收缩压", double.Parse(bpSys));
            context.NumericParameters.Add("肱动脉舒张压", double.Parse(bpDia));

            var filteredProblems = new List<ClinicalProblemDefinition>();
            using (var db = new KBEntities())
            {
                filteredProblems.Add(db.ClinicalProblemDefinition.FirstOrDefault(x => x.Name == "心肌梗死"));
                filteredProblems.Add(db.ClinicalProblemDefinition.FirstOrDefault(x => x.Name == "高血压"));

                var triggeredProblems = (new DecisionSupportService()).Reason(filteredProblems, context);

                var result = "发现的临床问题：";
                triggeredProblems.ToList().ForEach(x => result = result + x + ";");

                sw.Stop();
                ViewBag.Performance = "Total consumed time (ms): " + sw.ElapsedMilliseconds;
                return View(triggeredProblems);
            }
        }

        /// <summary>
        /// Test DecisionSupportService.Notify() I/F
        /// </summary>
        /// <param name="id">event id</param>
        /// <returns>log</returns>
        [HttpPost]
        public ActionResult Notify(int id)
        {
            //
            // We can use TraceListener to collect trace infomation.
            // However, the collected info cannot be displayed line by line in View. Therefore, we use Dictionary as log.
            //
            // var writer = new StringWriter();
            // TraceListener listener = new TextWriterTraceListener(writer);
            // Trace.Listeners.Add(listener);
            // 
            // var svc = new DecisionSupportService() as IDecisionSupportService;
            // svc.Notify(id);
            //
            // listener.Flush();
            // Trace.Listeners.Remove(listener);
            // var result = writer.ToString();
            // return View(writer);

            return View((new DecisionSupportService()).Notify(id));
        }

        [HttpPost]
        public ActionResult UpdateProblemState(int id, string operation, string user)
        {
            if (db.ClinicalProblemInstance.Find(id) == null)
            {
                return RedirectToAction("Error", "Encounter", new { messag = "Cannot find Clinical Problem Instance" });
            }

            var encounter = db.ClinicalProblemInstance.Find(id).Encounter;
            if (encounter == null)
            {
                return RedirectToAction("Error", "Encounter", new { messag = "Cannot find Encounter for Clinical Problem Instance" });
            }

            (new DecisionSupportService()).UpdateProblemState(id, operation, user);            
            return RedirectToAction("Details", new { id = encounter.Id });
        }        

        /// <summary>
        /// Clear Up all existing clinical problem instances associated with the encounter.
        /// For Demo Use ONLY.
        /// </summary>
        /// <param name="id">encounter id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClearUp(int id)
        {
            var encounter = db.Encounter.Find(id);
            if (encounter == null)
                return HttpNotFound();

            var instances = db.ClinicalProblemInstance.Where(x => x.Encounter.Id == encounter.Id).ToList();
            for (int j = instances.Count() - 1; j >= 0; j--)
            {
                var instance = instances[j];

                //
                // Slay associated ChangeRecords
                for (int k = instance.ChangeRecord.Count - 1; k >= 0; k--)
                {
                    var record = instance.ChangeRecord.ElementAt(k);
                    //
                    // Slay associated Facts
                    for (int i = record.Fact.Count - 1; i >= 0; i--)
                    {
                        var fact = record.Fact.ElementAt(i);
                        record.Fact.Remove(fact);
                        db.Fact.Remove(fact);
                    }
                    db.ChangeRecord.Remove(record);
                    instance.ChangeRecord.Remove(record);
                }

                //
                // Then suicide
                db.ClinicalProblemInstance.Remove(instance);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }

        //
        // GET: /EMR/Home/Details/5

        public ActionResult Details(int id)
        {
            var encounter = db.Encounter.Find(id);
            if (encounter == null)
                return HttpNotFound();
            ViewBag.DB = this.db;
            return View(encounter);
        }

        public ActionResult Map()
        {
            return View();
        }
    }
}