using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuggetAppTorneos.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AppTorneos.Services
{
    public class ServiceTournament
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string ApiAppTorneos;

        public ServiceTournament(IConfiguration configuration)
        {
            this.ApiAppTorneos =
                configuration.GetValue<string>("ApiUrls:ApiAppTorneos");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<string> GetTokenAsync
            (string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth/login";
                client.BaseAddress = new Uri(this.ApiAppTorneos);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    UserName = username,
                    Password = password
                };
                string jsonModel = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonModel, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(data);
                    string token =
                        jsonObject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiAppTorneos);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApiAsync<T>
            (string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiAppTorneos);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
                    ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<HttpStatusCode> InsertApiAsync<T>(string request, T objeto) 
        { 
            using (HttpClient client = new HttpClient()) 
            { 
                client.BaseAddress = new Uri(this.ApiAppTorneos); 
                client.DefaultRequestHeaders.Clear(); 
                client.DefaultRequestHeaders.Accept.Add(this.Header); 
                string json = JsonConvert.SerializeObject(objeto); 
                StringContent content = 
                    new StringContent(json, Encoding.UTF8, "application/json"); 
                HttpResponseMessage response = await client.PostAsync(request, content); 
                return response.StatusCode; 
            } 
        }

        private async Task<HttpStatusCode> InsertApiAsync<T>(string request, T objeto, string token) 
        { 
            using (HttpClient client = new HttpClient()) 
            { 
                client.BaseAddress = new Uri(this.ApiAppTorneos); 
                client.DefaultRequestHeaders.Clear(); 
                client.DefaultRequestHeaders.Accept.Add(this.Header); 
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token); 
                string json = JsonConvert.SerializeObject(objeto); 
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json"); 
                HttpResponseMessage response = await client.PostAsync(request, content); 
                return response.StatusCode; 
            } 
        }

        private async Task<HttpStatusCode> DeleteApiAsync<T>(string request, string token) 
        { 
            using (HttpClient client = new HttpClient()) 
            { 
                client.BaseAddress = new Uri(this.ApiAppTorneos); 
                client.DefaultRequestHeaders.Clear(); 
                client.DefaultRequestHeaders.Accept.Add(this.Header); 
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token); 
                HttpResponseMessage response = await client.DeleteAsync(request); 
                return response.StatusCode; 
            } 
        }

        //metodo protegido
        public async Task<List<Equipo>> GetEquipos(string token)
        {
            string request = "/api/Equipo/GetEquipos";
            List<Equipo> equipos =
                await this.CallApiAsync<List<Equipo>>(request, token);
            return equipos;
        }

        public async Task<Equipo> FindEquipo(int idequ)
        {
            string request = "/api/Equipo/FindEquipo/"+ idequ;
            Equipo equipo =
                await this.CallApiAsync<Equipo>(request);
            return equipo;
        }

        public async Task InsertEquipo(Equipo equipo, string token)
        {
            string request = "/api/Equipo/InsertEquipo";
            await this.InsertApiAsync<Equipo>(request, equipo, token);
        }

        public async Task DeleteEquipos(string token, int ideq)
        {
            string request = "/api/Equipo/DeleteEquipo/"+ideq;
            await this.DeleteApiAsync<Equipo>(request, token);
        }

        public async Task ConfirmarEquipos(string token, int idequi, bool conf2, bool conf3)
        {
            string request = "/api/Equipo/ConfirmarEquipo/"+idequi+"/"+conf2+"/"+conf3;
            await this.InsertApiAsync<Equipo>(request,null, token);
        }

        //LIGAS
        public async Task<List<Liga>> GetLigas(string token)
        {
            string request = "/api/Liga";
            List<Liga> ligas =
                await this.CallApiAsync<List<Liga>>(request, token);
            return ligas;
        }

        public async Task<Liga> GetLiga(string token, int idlig)
        {
            string request = "/api/Liga/GetLiga/"+ idlig;
            Liga liga =
                await this.CallApiAsync<Liga>(request, token);
            return liga;
        }

        public async Task<List<Equipo>> GetInfoEquipoLiga(string token, int idli)
        {
            string request = "/api/Liga/GetInfoEqLi/" + idli;
            List<Equipo> equipos =
                await this.CallApiAsync<List<Equipo>>(request, token);
            return equipos;
        }

        public async Task<List<Liga>> FiltrarLiga(string token, string nom)
        {
            string request = "/api/Liga/FiltrarLiga/" + nom;
            List<Liga> ligas =
                await this.CallApiAsync<List<Liga>>(request, token);
            return ligas;
        }

        public async Task SolicitarAcceso(string token, int idliga, int ideq)
        {
            string request = "/api/Liga/SolicitarAcceso/" + idliga+"/"+ideq;
            await this.InsertApiAsync<Liga>(request, null, token);
        }

        public async Task AccionAccesoLiga(string token, bool conf, int idins)
        {
            string request = "/api/Liga/AccionAcceso/" + conf + "/" + idins;
            await this.InsertApiAsync<Liga>(request, null, token);
        }

        public async Task<List<EquipoLiga>> GetEquXLig(string token, int idl)
        {
            string request = "/api/Liga/GetEquXLig/" + idl ;
            List<EquipoLiga> apuntados =
                await this.CallApiAsync<List<EquipoLiga>>(request, token);
            return apuntados;
        }

        public async Task CrearLiga(string token, string nombre, int idusuario, int idequipo)
        {
            string request = "/api/Liga/CrearLiga/" + nombre + "/" + idusuario + "/"+ idequipo;
            await this.InsertApiAsync<Liga>(request,null, token);
        }

        public async Task AnadirEquipoLiga(string token, int idlig1, int idequ1)
        {
            string request = "/api/Liga/AnadirEquipoLiga/" + idlig1 + "/" + idequ1;
            await this.InsertApiAsync<EquipoLiga>(request, null, token);
        }

        public async Task<List<EquipoLiga>> GetEquipoLigaApuntados(string token, int ideq)
        {
            string request = "/api/Liga/GetEquLiAp/"+ ideq;
            List<EquipoLiga> apuntados = await this.CallApiAsync <List<EquipoLiga>>(request, token);
            return apuntados;
        }

        public async Task EmpezarLiga(string token, int idliga, DateTime fecha)
        {
            string request = "/api/Liga/EmpezarLiga/" + idliga + "/" + fecha;
            await this.InsertApiAsync<EquipoLiga>(request, null, token);
        }

        public async Task<string> Login(LoginModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Usuario/Login";
                client.BaseAddress = new Uri(this.ApiAppTorneos);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string jsonModel = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonModel, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(data);
                    string token =
                        jsonObject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task Registrar(string usuariotag, string nombre, string email, string contrasenia)
        {
            string request = "/api/Usuario/InsertarUsu/" + usuariotag + "/" + nombre + "/" + email + "/"+ contrasenia;
            await this.InsertApiAsync<User>(request, null);
        }

        public async Task<int> FindUsu(string token, string tag)
        {
            string request = "/api/Usuario/FindUsu/"+tag;
            int num = await this.CallApiAsync<int>(request, token);
            return num;
        }

        public async Task<User> GetPerfilUsuario(string token)
        {
            string request = "/api/Usuario/PerfilUsuario";
            return await this.CallApiAsync<User>(request, token);
        }
    }
}
