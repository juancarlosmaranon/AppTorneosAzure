using Microsoft.AspNetCore.Mvc;
using System.Text;
using AppTorneos.Extensions;
using AppTorneos.Helpers;
using Newtonsoft.Json.Linq;
using NuggetAppTorneos.Models;
using AppTorneos.Services;

namespace AppTorneos.Controllers
{
    public class LoginUsuarioController : Controller
    {
        private HelperApiBrawl helper;
        private ServiceTournament service;

        public LoginUsuarioController(HelperApiBrawl helper, ServiceTournament service)
        {
            this.helper = helper;
            this.service = service;
        }

        public IActionResult InicioPagina()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InicioPagina(string accion, string nombre, string usuariotag, string email, string contrasenia)
        {
            if (accion == "registro")
            {
                //AQUI HAZ EL REGISTRO
                ViewData["MENSAJE"] = "INIASTE REGISTRO";
                if(await helper.TokenApi(usuariotag) == false)
                {
                    ViewData["ERROR"] = "Error no existe el usuario";
                }
                else
                {
                    await this.service.Registrar(usuariotag, nombre, email, contrasenia);
                }

                

            }
            else if (accion == "iniciosesion")
            {
                LoginModel model = new LoginModel
                {
                    UserName = email,
                    Password = contrasenia
                };

                string token = await this.service.Login(model);
                if (token != null)
                {
                    //GUARDAR EN SESION
                    ViewData["MENSAJE"] = "INIASTE SESION";
                    HttpContext.Session.SetString("TOKEN", token);

                    HttpContext.Session.SetObject("USUARIO", await this.service.GetPerfilUsuario(token));
                    //return a vista de menu de inicio
                    return RedirectToAction("MenuInicio", "Inicio");
                }
                else
                {
                    ViewData["MENSAJE"] = "ERROR EN LAS CREDENCIALES";
                }
                //AQUI EL INICIO DE SESION
                
                //SI COINCIDE EL USUARIO, GUARDA EN SESION Y REDIRIGE SIGUIENTE VISTA
                //SI NO DEVUELVELE A LA VISTA CON ERROR
            }
            return View();
        }

        public IActionResult CerrarSession()
        {
            HttpContext.Session.Remove("USUARIO");
            HttpContext.Session.Remove("TOKEN");
            return RedirectToAction("InicioPagina");
        }

        public async Task<IActionResult> PerfilUsuario()
        {
            Perfil perf = new Perfil();

            User usu = HttpContext.Session.GetObject<User>("USUARIO");
            if (usu != null)
            {
                JObject jsonusu = await this.helper.InfoUsuarioXTag(usu.UsuarioTag);


                perf.Tag = jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "tag" && x.Type.ToString() == "String").FirstOrDefault().ToString();

                perf.Nombre = jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "name").FirstOrDefault().ToString();
                perf.Trofeos = int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "trophies").FirstOrDefault().ToString());
                perf.MaximoTr = int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "highestTrophies").FirstOrDefault().ToString());
                perf.VictoriasTotales = int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "3vs3Victories").FirstOrDefault().ToString())
                    + int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "soloVictories").FirstOrDefault().ToString())
                    + int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "duoVictories").FirstOrDefault().ToString());

                return View(perf);
            }
            else
            {
                return RedirectToAction("InicioPagina", "LoginUsuario");
            }
            
        }
    }
}
