using ReserveAqui.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ReserveAqui.Controllers
{

    [Authorize(Roles = "Admin")]
    public class HospedesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Hospedes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Hospede hospede = db.Hospedes.Find(id);
            if (hospede == null) return HttpNotFound();

            return View(hospede);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hospede hospede)
        {
            if (ModelState.IsValid)
            {
                // Verifica duplicidade de CPF
                if (db.Hospedes.Any(h => h.CPF == hospede.CPF))
                {
                    ModelState.AddModelError("CPF", "Hóspede com este CPF já existe.");
                    return View(hospede);
                }

                db.Hospedes.Add(hospede);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Hóspede criado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(hospede);
        }

        // GET: Hospedes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Hospede hospede = db.Hospedes.Find(id);
            if (hospede == null) return HttpNotFound();

            return View(hospede);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hospede hospede)
        {
            if (ModelState.IsValid)
            {
                // Verifica duplicidade de CPF
                if (db.Hospedes.Any(h => h.CPF == hospede.CPF && h.Id != hospede.Id))
                {
                    ModelState.AddModelError("CPF", "Outro hóspede já está registrado com este CPF.");
                    return View(hospede);
                }

                db.Entry(hospede).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Hóspede atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(hospede);
        }

        // GET: Hospedes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Hospede hospede = db.Hospedes.Find(id);
            if (hospede == null) return HttpNotFound();

            return View(hospede);
        }

        // POST: Hospedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hospede hospede = db.Hospedes.Find(id);
            if (hospede == null) return HttpNotFound();

            db.Hospedes.Remove(hospede);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Hóspede deletado com sucesso.";
            return RedirectToAction("Index");
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
