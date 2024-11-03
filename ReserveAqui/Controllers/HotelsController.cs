using ReserveAqui.Models;
using System.Collections.Generic;
using System.Data.Entity;
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
            var hotels = _db.Hoteis.Include(h => h.Quartos).ToList() ?? new List<Hotel>();
            return View(hotels);
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
                // Verifica se o arquivo foi carregado
                if (imagemUrl != null)
                {
                    if (!imagemUrl.ContentType.StartsWith("image"))
                    {
                        ModelState.AddModelError("", "O arquivo deve ser uma imagem.");
                        return View(hotel);
                    }

                    if (imagemUrl.ContentLength > 5 * 1024 * 1024) // Limite de 5MB
                    {
                        ModelState.AddModelError("", "A imagem não pode exceder 5MB.");
                        return View(hotel);
                    }

                    // Adicionar o processamento do arquivo de imagem, como salvá-lo no servidor
                    // e definir o caminho no modelo hotel.ImagemUrl, caso necessário.
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
