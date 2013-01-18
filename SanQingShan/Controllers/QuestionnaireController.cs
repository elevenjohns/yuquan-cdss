using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SanQingShan.Data;
using SanQingShan.Models;

namespace SanQingShan.Controllers
{
    public class QuestionnaireController : Controller
    {
        private QuestionnaireContainer db = new QuestionnaireContainer();

        /// <summary>
        /// seed questionnaire
        /// </summary>
        public string Seed()
        {
            try
            {
                QuestionnaireInitializer.Seed();
            }
            catch (Exception ex)
            {
                return "初始化问卷失败. "+ ex.Message;
            }
            return "初始化问卷成功";
        }

        //
        // GET: /Questionnaire/

        public ActionResult Index()
        {
            var questionnaire = db.QuestionnaireSet.FirstOrDefault();
            if (questionnaire != null)
                return View(questionnaire);
            else
                return RedirectToAction("Seed");
        }

        //
        // GET: /Questionnaire/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Questionnaire/Create

        public ActionResult Create()
        {
            var answer = new Answer(db.ConceptSet.FirstOrDefault() as Question);
            return View(answer);
        } 

        //
        // POST: /Questionnaire/Create

        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Questionnaire/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Questionnaire/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Questionnaire/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Questionnaire/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
