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
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = db.Hoteis.Include(h => h.Quartos).ToList();
            return View(hotels);
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hotel = db.Hoteis.Include(h => h.Quartos).FirstOrDefault(h => h.Id == id);
            if (hotel == null)
                return HttpNotFound();

            return View(hotel);
        }

        // GET: Hotels/Details/5
        public ActionResult MoreDetails(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hotel = db.Hoteis.Include(h => h.Quartos).FirstOrDefault(h => h.Id == id);
            if (hotel == null)
                return HttpNotFound();

            return View(hotel);
        }

        // GET: Hotels/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Hotel hotel, HttpPostedFileBase imagemUrl)
        {
            if (ModelState.IsValid)
            {
                if (imagemUrl != null && imagemUrl.ContentType.StartsWith("image") && imagemUrl.ContentLength <= 5 * 1024 * 1024)
                {
                    var imagePath = "/Content/Images/" + imagemUrl.FileName;
                    var serverPath = Server.MapPath(imagePath);

                    // Verificar se o diretório existe e, caso não, criar o diretório
                    var directory = Path.GetDirectoryName(serverPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    imagemUrl.SaveAs(serverPath);
                    hotel.ImagemUrl = imagePath;
                }

                db.Hoteis.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hotel = db.Hoteis.Find(id);
            if (hotel == null)
                return HttpNotFound();

            return View(hotel);
        }

        // POST: Hotels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Hotel hotel, HttpPostedFileBase imagemUrl)
        {
            if (ModelState.IsValid)
            {
                if (imagemUrl != null && imagemUrl.ContentType.StartsWith("image") && imagemUrl.ContentLength <= 5 * 1024 * 1024)
                {
                    var imagePath = "/Content/Images/" + imagemUrl.FileName;
                    var serverPath = Server.MapPath(imagePath);

                    // Excluir a imagem antiga se ela existir
                    var oldHotel = db.Hoteis.AsNoTracking().FirstOrDefault(h => h.Id == hotel.Id);
                    if (oldHotel != null && !string.IsNullOrEmpty(oldHotel.ImagemUrl))
                    {
                        var oldImagePath = Server.MapPath(oldHotel.ImagemUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Salvar a nova imagem
                    imagemUrl.SaveAs(serverPath);
                    hotel.ImagemUrl = imagePath;
                }

                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        // GET: Hotels/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hotel = db.Hoteis.Find(id);
            if (hotel == null)
                return HttpNotFound();

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var hotel = db.Hoteis.Find(id);
            if (hotel != null)
            {
                // Remover a imagem do hotel se existir
                if (!string.IsNullOrEmpty(hotel.ImagemUrl))
                {
                    var imagePath = Server.MapPath(hotel.ImagemUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                db.Hoteis.Remove(hotel);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Hotels/AddRoom/1
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddRoom(int hotelId)
        {
            var hotel = db.Hoteis.Find(hotelId);
            if (hotel == null) return HttpNotFound();

            ViewBag.HotelId = hotelId;
            return View(new Quarto { HotelId = hotelId });
        }

        // POST: Hotels/AddRoom
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoom(Quarto room)
        {
            if (ModelState.IsValid)
            {
                db.Quartos.Add(room);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = room.HotelId });
            }

            ViewBag.HotelId = room.HotelId;
            return View(room);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditRoom(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var room = db.Quartos.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            ViewBag.HotelId = new SelectList(db.Hoteis, "Id", "Nome", room.HotelId);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoom(Quarto room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = room.HotelId });
            }
            return View(room);
        }

        public ActionResult ViewHotelHospede()
        {
            var hotels = db.Hoteis.Include(h => h.Quartos).ToList();
            return View(hotels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
