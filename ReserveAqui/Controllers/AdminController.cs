using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Logging;
using ReserveAqui.Models;
using WebGrease;

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
