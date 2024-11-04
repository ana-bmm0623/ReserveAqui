using ReserveAqui.Models;
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

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = _db.Hoteis.Include(h => h.Quartos).ToList();
            return View(hotels);
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hotel = _db.Hoteis.Include(h => h.Quartos).FirstOrDefault(h => h.Id == id);
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
                    imagemUrl.SaveAs(Server.MapPath(imagePath));
                    hotel.ImagemUrl = imagePath;
                }

                _db.Hoteis.Add(hotel);
                _db.SaveChanges();
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

            var hotel = _db.Hoteis.Find(id);
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
                    imagemUrl.SaveAs(Server.MapPath(imagePath));
                    hotel.ImagemUrl = imagePath;
                }

                _db.Entry(hotel).State = EntityState.Modified;
                _db.SaveChanges();
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

            var hotel = _db.Hoteis.Find(id);
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
            var hotel = _db.Hoteis.Find(id);
            if (hotel != null)
            {
                _db.Hoteis.Remove(hotel);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Hotels/AddRoom/1
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddRoom(int hotelId)
        {
            var hotel = _db.Hoteis.Find(hotelId);
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
                _db.Quartos.Add(room);
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = room.HotelId });
            }

            ViewBag.HotelId = room.HotelId;
            return View(room);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditRoom(int id) // Recebe o ID do quarto específico
        {
            var room = _db.Quartos.Find(id); // Busca o quarto pelo ID
            if (room == null)
            {
                return HttpNotFound();
            }

            ViewBag.HotelId = new SelectList(_db.Hoteis, "Id", "Nome", room.HotelId);
            return View(room); // Passa o quarto específico para a View
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoom(Quarto room)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(room).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = room.HotelId });
            }
            return View(room);
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
