using System.Linq;
using System.Web.Mvc;
using WuKeSong.Areas.Music.Models;

namespace WuKeSong.Areas.Music.Controllers
{
    public class GenreController : Controller
    {
        private MusicDBContext db = new MusicDBContext();

        //
        // GET: /Music/Genre/

        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }
    }
}
