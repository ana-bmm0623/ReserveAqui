using ReserveAqui.Models;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReserveAqui.Controllers
{

    public class QuartosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Reserve()
        {
            return View();
        }

        public ActionResult Index()
        {
            var quartos = db.Quartos.Include(q => q.Hotel).Where(q => q.Disponibilidade == true).ToList();
            return View(quartos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create(int hotelId)
        {
            ViewBag.HotelId = hotelId;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quarto quarto, HttpPostedFileBase ImagemUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImagemUrl != null && ImagemUrl.ContentLength > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(ImagemUrl.FileName).ToLower();

                    if (allowedExtensions.Contains(extension) && ImagemUrl.ContentLength <= 2 * 1024 * 1024) // Limite de 2 MB
                    {
                        var fileName = $"{quarto.Id}_{ImagemUrl.FileName}";
                        var path = Path.Combine(Server.MapPath("~/Images/Quartos"), fileName);
                        ImagemUrl.SaveAs(path);
                        quarto.ImagemUrl = $"/Images/Quartos/{fileName}";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Por favor, envie uma imagem válida com menos de 2 MB.");
                    }
                }


                db.Quartos.Add(quarto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hoteis, "Id", "Nome", quarto.HotelId);
            return View(quarto);
        }


        [HttpGet]
        public ActionResult FilterByCapacity(int capacity)
        {
            var filteredQuartos = db.Quartos.Where(q => q.CapacidadeMaxima >= capacity && q.Disponibilidade).ToList();
            return View("Index", filteredQuartos);
        }

        // GET: Quartos/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = db.Quartos.Find(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hoteis, "Id", "Nome", quarto.HotelId);
            return View(quarto);
        }

        // POST: Quartos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Quarto quarto, HttpPostedFileBase ImagemUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImagemUrl != null && ImagemUrl.ContentLength > 0)
                {
                    var fileName = $"{quarto.Id}_{ImagemUrl.FileName}";
                    var path = Path.Combine(Server.MapPath("~/Images/Quartos"), fileName);

                    // Remover a imagem antiga se existente
                    if (!string.IsNullOrEmpty(quarto.ImagemUrl))
                    {
                        var oldPath = Server.MapPath(quarto.ImagemUrl);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    ImagemUrl.SaveAs(path);
                    quarto.ImagemUrl = $"/Images/Quartos/{fileName}";
                }

                else
                {
                    // Preserva o caminho da imagem anterior se nenhuma nova imagem foi enviada
                    var existingQuarto = db.Quartos.AsNoTracking().FirstOrDefault(q => q.Id == quarto.Id);
                    quarto.ImagemUrl = existingQuarto?.ImagemUrl;
                }

                db.Entry(quarto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hoteis, "Id", "Nome", quarto.HotelId);
            return View(quarto);
        }


        // GET: Quartos/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = db.Quartos.Find(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            return View(quarto);
        }

        // POST: Quartos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Quarto quarto = db.Quartos.Find(id);
            db.Quartos.Remove(quarto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var quarto = db.Quartos.Find(id);
            if (quarto == null)
                return HttpNotFound();

            return View(quarto);

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
