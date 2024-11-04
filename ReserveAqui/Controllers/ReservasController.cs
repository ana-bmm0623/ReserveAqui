using Microsoft.AspNet.Identity;
using ReserveAqui.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ReserveAqui.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index(int quartoId)
        {
            ViewBag.QuartoId = quartoId;
            var reservas = db.Reservas.Include(r => r.Quarto).Include(r => r.Hospede).ToList();
            return View(reservas);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Create(int? quartoId)
        {
            if (!quartoId.HasValue)
            {
                TempData["ErrorMessage"] = "ID do quarto não fornecido.";
                return RedirectToAction("Index");
            }

            var quarto = db.Quartos.Find(quartoId.Value);
            if (quarto == null || !quarto.Disponibilidade)
            {
                TempData["ErrorMessage"] = "Quarto não encontrado ou não disponível para reserva.";
                return RedirectToAction("Index");
            }

            // Populando ViewBags necessários para a view
            ViewBag.QuartoId = quartoId;
            ViewBag.Quartos = db.Quartos.Select(q => new { q.Id, q.NumeroIdentificacao }).ToList();
            ViewBag.Hospedes = db.Hospedes.Select(h => new { h.Id, h.NomeCompleto }).ToList();

            return View(new Reserva { QuartoId = quartoId.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var quarto = db.Quartos.Find(reserva.QuartoId);

                if (!quarto.Disponibilidade)
                {
                    ModelState.AddModelError("", "O quarto selecionado não está disponível para reserva.");
                    return View(reserva);
                }

                quarto.Disponibilidade = false;
                reserva.ApplicationUserId = User.Identity.GetUserId();  // Associa a reserva ao usuário atual
                db.Reservas.Add(reserva);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Reserva criada com sucesso!";
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        [Authorize]
        public ActionResult Cancel(int id)
        {
            var reserva = db.Reservas.Include(r => r.Quarto).FirstOrDefault(r => r.Id == id);

            if (reserva == null || reserva.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Acesso não autorizado.");
            }

            reserva.Cancelada = true;
            reserva.Quarto.Disponibilidade = true;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Reserva cancelada com sucesso.";
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reserva = db.Reservas.Find(id);
            if (reserva == null) return HttpNotFound();

            if (reserva.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Acesso não autorizado.");
            }

            ViewBag.HospedeId = new SelectList(db.Hospedes, "Id", "NomeCompleto", reserva.HospedeId);
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,QuartoId,HospedeId,QuantidadePessoas,DataEntrada,DataSaida,CheckInRealizado,CheckOutRealizado,Cancelada")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var reservaAtual = db.Reservas.AsNoTracking().FirstOrDefault(r => r.Id == reserva.Id);

                if (reservaAtual == null || reservaAtual.ApplicationUserId != User.Identity.GetUserId())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Acesso não autorizado.");
                }

                reserva.ApplicationUserId = reservaAtual.ApplicationUserId;

                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Reserva atualizada com sucesso!";
                return RedirectToAction("Index");
            }

            ViewBag.HospedeId = new SelectList(db.Hospedes, "Id", "NomeCompleto", reserva.HospedeId);
            return View(reserva);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reserva = db.Reservas.Find(id);
            if (reserva == null || reserva.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Acesso não autorizado.");
            }

            return View(reserva);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reserva = db.Reservas.Find(id);

            if (reserva == null || reserva.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Acesso não autorizado.");
            }

            db.Reservas.Remove(reserva);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Reserva deletada com sucesso.";
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
