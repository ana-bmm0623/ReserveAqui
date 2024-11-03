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
            return View();
        }

        // Ação privada - apenas usuários autenticados podem acessar
        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
    }

}