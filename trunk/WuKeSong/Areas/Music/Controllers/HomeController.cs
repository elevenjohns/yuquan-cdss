using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WuKeSong.Areas.Music.Models;
using System.Web;
using System.IO;

namespace WuKeSong.Areas.Music.Controllers
{ 
    public class HomeController : Controller
    {
        private MusicDBContext db = new MusicDBContext();

        //
        // GET: /Music/

        public ViewResult Index()
        {
            db.SaveChanges();
            return View(db.Music.ToList());
        }

        // rename parameter to id to use as route data (URL segment). e.g. .../Search/Keyword
        public ActionResult Search(String id)
        {
            String searchString = id;
            var music = from m in db.Music select m; //create LINQ query
            if (!String.IsNullOrEmpty(searchString))
                music = music.Where(s => (
                    s.Title.Contains(searchString)||
                    s.Genre.Contains(searchString)||
                    s.Author.Contains(searchString)||
                    s.Comment.Contains(searchString)));
            return View(music);
        }

        [HttpPost]
        public String Search(FormCollection fc, String searchString) //FormCollection is not used, but only to give a different method name signature.
        {
            // @using (Html.BeginForm("Search","Music",FormMethod.Get)) 
            return "This page should not be shown!";
        }

        //
        // GET: /Music/Details/5

        public ViewResult Details(int id)
        {
            Models.Music music = db.Music.Find(id);
            return View(music);
        }

        //
        // GET: /Music/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Music/Create

        [HttpPost]
        public ActionResult Create(Models.Music music)
        {
            if (ModelState.IsValid)
            {
                db.Music.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(music);
        }
        
        //
        // GET: /Music/Edit/5
 
        public ActionResult Edit(int id = 0)
        {
            Models.Music music = db.Music.Find(id);
            if (music == null)
                return HttpNotFound();

            ViewBag.GenreList= new MultiSelectList(db.Genres.ToList(), "ID", "Title");

            return View(music);
        }

        ////
        //// POST: /Music/Edit/5

        /// <summary>
        /// Use PropertyInfo to populate object by FormCollection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            int id = int.Parse(collection.GetValues("ID")[0]);
            var music = db.Music.Find(id);
            if (music == null)
            {
                return HttpNotFound();                
            }

            // Iterate all public string properties to get updated
            foreach (PropertyInfo propertyInfo in music.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite && propertyInfo.Name!="ID" && propertyInfo.PropertyType == typeof(string))
                {
                    // the basic assumption here is that all properties are string and one dimensional
                    propertyInfo.SetValue(music, collection.GetValues(propertyInfo.Name)[0],null);
                }
            }

            string[] genreIDs = collection.GetValues("Genres");
            if (genreIDs != null)
            {
                foreach (var item in genreIDs)
                {
                    var genre = db.Genres.Find(int.Parse(item));
                    if (genre != null)
                        music.Genres.Add(genre);
                }
            }

            var src = collection.GetValues("file")[0] as string;
            if (System.IO.File.Exists(src))
            {
                // extract only the fielname
                var fileName = Guid.NewGuid()+Path.GetExtension(src);
                // store the file inside ~/App_Data/uploads folder
                var des = Path.Combine(Server.MapPath("~/upload"), fileName);
                System.IO.File.Copy(src, des);
                music.SheetMusic = "/upload/"+fileName;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

#if false
        // For testing the DeepModelBinder
        [HttpPost]
        [ActionName("Edit"), CustomActionNameSelector("SaveMusic")]
        public ActionResult SaveMusic(int id)
        {
            var music = db.Music.Find(id);
            this.TryUpdateModel(music);
            
            // If valid, save the updated order:
            if (ModelState.IsValid)
            {
                db.SaveChanges();
            }
            else
            {
                return View(id);
            }

            return RedirectToAction("Edit", new { Id = id });
        }

        [HttpPost]
        [ActionName("Edit"), CustomActionNameSelector("AddGenre")]
        public ActionResult AddGenre(int id)
        {
            var music = db.Music.Find(id);
            music.Genres.Add("New Genre");

            return RedirectToAction("Edit", new { Id = id });
        }
#endif

        //
        // GET: /Music/Delete/5
 
        // [ActionName("Remove")]
        public ActionResult Delete(int id=0)
        {
            Models.Music music = db.Music.Find(id);
            if (music == null)
                return HttpNotFound();
            return View(music);
        }

        //
        // POST: /Music/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id=0)
        {            
            Models.Music music = db.Music.Find(id);
            if (music == null)
                return HttpNotFound();
            db.Music.Remove(music);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}