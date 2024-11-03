using ReserveAqui.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReserveAqui.Controllers
{

    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            // Lógica para obter e retornar a lista de hotéis
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hotel = _db.Hoteis.Include(h => h.Quartos).FirstOrDefault(h => h.Id == id);
            if (hotel == null)
                return HttpNotFound();

            return View(hotel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hotel hotel, HttpPostedFileBase imagemUrl)
        {
            if (ModelState.IsValid)
            {
                if (imagemUrl != null && imagemUrl.ContentLength > 0)
                {
                    var fileName = $"{hotel.Id}_{Path.GetFileName(imagemUrl.FileName)}";
                    var path = Path.Combine(Server.MapPath("~/Images/Hotels"), fileName);
                    imagemUrl.SaveAs(path);

                    hotel.ImagemUrl = $"/Images/Hotels/{fileName}";
                }

                _db.Hoteis.Add(hotel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
