using AppTorneos.Extensions;
using Microsoft.AspNetCore.Mvc;
using NuggetAppTorneos.Models;

namespace AppTorneos.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult MenuInicio()
        {
            User usuario = HttpContext.Session.GetObject<User>("USUARIO");
            if(usuario != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("InicioPagina", "LoginUsuario");
            }
        }
    }
}
