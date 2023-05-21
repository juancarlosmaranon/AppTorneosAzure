using AppTorneos.Extensions;
using AppTorneos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NuggetAppTorneos.Models;

namespace AppTorneos.Controllers
{
    public class EquiposController : Controller
    {
        private ServiceTournament service;

        public EquiposController(ServiceTournament service)
        {
            this.service = service;
        }

        public async Task<IActionResult> MisEquipos()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            int idj1 = HttpContext.Session.GetObject<User>("USUARIO").IdUsuario;

            List<Equipo> equipos = await this.service.GetEquipos(token);

            bool pendientes = false;

            foreach (Equipo equip in equipos)
            {
                if (equip.Jugador1 != idj1)
                {
                    if (equip.Jugador2 == idj1)
                    {
                        if (equip.ConfirmJug2 == -1)
                        {
                            pendientes = true;
                            break;
                        }

                    }else if(equip.Jugador3 == idj1)
                    {
                        if (equip.ConfirmJug3 == -1)
                        {
                            pendientes = true;
                            break;
                        }
                    }
                }
            }

            ViewData["PENDIENTES"] = pendientes;
            return View(equipos);
        }

        [HttpPost]
        public async Task<IActionResult> MisEquipos(string nombreEquipo, string jugador2, string jugador3)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            int idj1 = HttpContext.Session.GetObject<User>("USUARIO").IdUsuario;

            int idjug2 = await this.service.FindUsu(token,jugador2);
            int idjug3 = await this.service.FindUsu(token,jugador3);

            if (idjug2 != 0 && idjug3 != 0 && idjug3 != idjug2 && idjug2 != idj1 && idjug3 != idj1)
            {
                Equipo equipo = new Equipo
                {
                    Nombre = nombreEquipo,
                    Jugador1 = idj1,
                    Jugador2 = idjug2,
                    Jugador3 = idjug3
                };

                await this.service.InsertEquipo(equipo,token);
            }
            
            return RedirectToAction("MisEquipos", "Equipos");
        }

        public async Task<IActionResult> BorraEquipo(int idequipo)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            await this.service.DeleteEquipos(token,idequipo);
            return RedirectToAction("MisEquipos");
        }

        public async Task<IActionResult> ConfirmaEquipo(int idequipo, bool confirmacion2, bool confirmacion3)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            await this.service.ConfirmarEquipos(token,idequipo, confirmacion2, confirmacion3);
            return RedirectToAction("MisEquipos");
        }
    }
}
