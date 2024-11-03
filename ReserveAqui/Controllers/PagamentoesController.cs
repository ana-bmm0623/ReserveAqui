using ReserveAqui.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ReserveAqui.Controllers
{

    public class PagamentoController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Pagamentos.Include(p => p.Reserva).ToList());
        }

        [HttpGet]
        public ActionResult Create(int reservaId)
        {
            // Verifica se a reserva existe
            var reserva = db.Reservas.Find(reservaId);
            if (reserva == null)
            {
                return HttpNotFound("Reserva não encontrada para associar o pagamento.");
            }

            ViewBag.ReservaId = reservaId;
            return View();
        }

        // POST: Pagamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                // Verifica se a reserva associada existe
                var reserva = db.Reservas.Find(pagamento.ReservaId);
                if (reserva == null)
                {
                    ModelState.AddModelError("", "Reserva não encontrada para associar o pagamento.");
                    return View(pagamento);
                }

                // Verifica se já existe um pagamento para a reserva
                var pagamentoExistente = db.Pagamentos.Any(p => p.ReservaId == pagamento.ReservaId);
                if (pagamentoExistente)
                {
                    ModelState.AddModelError("", "Já existe um pagamento associado a esta reserva.");
                    return View(pagamento);
                }

                pagamento.DataPagamento = DateTime.Now;
                db.Pagamentos.Add(pagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        // GET: Pagamento/Status
        public ActionResult Status(string status)
        {
            var pagamentos = db.Pagamentos.Include(p => p.Reserva).AsQueryable();

            if (status == "realizado")
            {
                pagamentos = pagamentos.Where(p => p.DataPagamento != null);
            }
            else if (status == "pendente")
            {
                pagamentos = pagamentos.Where(p => p.DataPagamento == null);
            }

            return View("Index", pagamentos.ToList());
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
