using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReserveAqui.Controllers
{
    [RequireHttps]
    // Exemplo de controlador com ações públicas e privadas
    public class HomeController : Controller
    {
        // Ação pública - qualquer usuário pode acessar
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }

            // Se não for admin, redireciona para a página inicial padrão
            return View();
        }


        // Ação privada - apenas usuários autenticados podem acessar
        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "admin" });
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            return View();
        }
    }

}