using System.Web.Mvc;

namespace ReserveAqui.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Redireciona para a lista de hotéis
        public ActionResult ManageHotels()
        {
            return RedirectToAction("Index", "Hotels");
        }

        // Redireciona para a lista de quartos
        public ActionResult ManageRooms()
        {

            return RedirectToAction("Index", "Quartos");
        }

        // Redireciona para a lista de hóspedes
        public ActionResult ManageGuests()
        {
            return RedirectToAction("Index", "Hospedes");
        }

        // Redireciona para a lista de serviços adicionais
        public ActionResult ManageServices()
        {
            return RedirectToAction("Index", "ServicoAdicionals");
        }

        // Redireciona para a lista de reservas
        public ActionResult ManageReservations()
        {
            return RedirectToAction("Index", "Reservas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

    }

}
