using AppTorneos.Extensions;
using AppTorneos.Services;
using Microsoft.AspNetCore.Mvc;
using NuggetAppTorneos.Models;

namespace AppTorneos.Controllers
{
    public class LigaController : Controller
    {
        private ServiceTournament service;

        public LigaController(ServiceTournament service)
        {
            this.service = service;
        }

        public async Task<IActionResult> MenuLigas()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            int idj1 = HttpContext.Session.GetObject<User>("USUARIO").IdUsuario;

            ViewData["EQUIPOSUSER"] = await this.service.GetEquipos(token);
            return View(await this.service.GetLigas(token));
        }
        [HttpPost]
        public async Task <IActionResult> MenuLigas(string accion, string nombre, int idequipo, DateTime fechainicio, int numequipos, int idliga) 
        {
            string token = HttpContext.Session.GetString("TOKEN");

            List<Liga> ligasfiltradas;
            if(accion== "CrearLiga")
            {
                ligasfiltradas = new List<Liga>();  
                await this.service.CrearLiga(token, nombre, HttpContext.Session.GetObject<User>("USUARIO").IdUsuario, idequipo);
                return RedirectToAction("MenuLigas","Liga");

            }else if(accion == "BuscarLiga")
            {
                ligasfiltradas = await this.service.FiltrarLiga(token,nombre);

            }
            else if (accion == "iniciarLiga")
            {
                ligasfiltradas = new List<Liga>();
                await this.service.EmpezarLiga(token,idliga,fechainicio);
                return RedirectToAction("MenuLigas", "Liga");
            }
            else
            {
                ligasfiltradas = new List<Liga>();
            }

            return View(ligasfiltradas);
        }

        
        public IActionResult CrearLigas() 
        {
            return View();
        }

        public async Task<IActionResult> _MenuAdminLiga(int idliga)
        {
            string token = HttpContext.Session.GetString("TOKEN");

            ViewData["LIGA"] = await this.service.GetLiga(token,idliga);
            ViewData["EQUIPOS"] = await this.service.GetInfoEquipoLiga(token,idliga);

            return PartialView("_MenuAdminLiga", await this.service.GetEquXLig(token,idliga));
        }

        public async Task<IActionResult> _TBodyLigas(int idequipo)
        {
            string token = HttpContext.Session.GetString("TOKEN");

            ViewData["LIGASAPUNTADO"] = await this.service.GetEquipoLigaApuntados(token,idequipo);
            ViewData["EQUIPO"] = await this.service.FindEquipo(idequipo);

            return PartialView("_TBodyLigas", await this.service.GetLigas(token));
        }

        public async Task<IActionResult> SolicitudAcceso(bool confirmado, int idinscrito)
        {
            string token = HttpContext.Session.GetString("TOKEN");

            await this.service.AccionAccesoLiga(token, confirmado, idinscrito);
            return RedirectToAction("MenuLigas","Liga");
        }

        public async Task<IActionResult> EnvioSolicitud(int idliga,int idequipo)
        {
            string token = HttpContext.Session.GetString("TOKEN");

            await this.service.SolicitarAcceso(token,idliga, idequipo);
            return RedirectToAction("MenuLigas", "Liga");
        }
    }
}
