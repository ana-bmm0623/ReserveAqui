using ReserveAqui.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace ReserveAqui.Controllers
{
    public class ServicoAdicionalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.ServicosAdicionais.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ServicoAdicional servico)
        {
            if (ModelState.IsValid)
            {
                if (servico.Valor <= 0)
                {
                    ModelState.AddModelError("Valor", "O valor do serviço deve ser positivo.");
                    return View(servico);
                }
                db.ServicosAdicionais.Add(servico);
                db.SaveChanges();
            }

            return View(servico);
        }

        public ActionResult AddToReserva(int servicoId, int reservaId)
        {
            // Verifica se a reserva existe
            var reserva = db.Reservas.Find(reservaId);
            if (reserva == null)
            {
                TempData["ErrorMessage"] = "Reserva não encontrada.";
                return RedirectToAction("Details", "Reservas", new { id = reservaId });
            }

            // Verifica se o serviço já está associado à reserva
            bool servicoExiste = db.ReservaServicosAdicionais
                .Any(r => r.ReservaId == reservaId && r.ServicoAdicionalId == servicoId);

            if (servicoExiste)
            {
                TempData["ErrorMessage"] = "Este serviço já foi adicionado a esta reserva.";
                return RedirectToAction("Details", "Reservas", new { id = reservaId });
            }

            var reservaServico = new ReservaServicoAdicional
            {
                ReservaId = reservaId,
                ServicoAdicionalId = servicoId
            };

            db.ReservaServicosAdicionais.Add(reservaServico);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Serviço adicional adicionado com sucesso!";
            return RedirectToAction("Details", "Reservas", new { id = reservaId });
        }

        // GET: ServicoAdicionals/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoAdicional servicoAdicional = db.ServicosAdicionais.Find(id);
            if (servicoAdicional == null)
            {
                return HttpNotFound();
            }
            return View(servicoAdicional);
        }

        // POST: ServicoAdicionals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Valor")] ServicoAdicional servicoAdicional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicoAdicional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicoAdicional);
        }

        // GET: ServicoAdicionals/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoAdicional servicoAdicional = db.ServicosAdicionais.Find(id);
            if (servicoAdicional == null)
            {
                return HttpNotFound();
            }
            return View(servicoAdicional);
        }

        // POST: ServicoAdicionals/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicoAdicional servicoAdicional = db.ServicosAdicionais.Find(id);
            db.ServicosAdicionais.Remove(servicoAdicional);
            db.SaveChanges();
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
