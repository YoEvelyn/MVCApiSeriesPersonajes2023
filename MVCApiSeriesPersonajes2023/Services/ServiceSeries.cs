using MVCApiSeriesPersonajes2023.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MVCApiSeriesPersonajes2023.Services
{
    public class ServiceSeries
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceSeries(string url)
        {
            this.UrlApi = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T>CallApiAsync<T>(string request)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if(response.IsSuccessStatusCode)
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

        #region SERIES
        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<Serie> FindSeriesAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;

        }
        public async Task<List<Personaje>> FindPersonajesSerieAsync(int idserie)
        {
            string request = "/api/series/personajeserie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        #endregion
        #region PERSONAJES
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        } 

        public async Task<Personaje> FindPersonajesAsync(int idpersonaje)
        {
            string request = "/api/personajes/" + idpersonaje;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task CreatePersonajeAsync(int id, string nombre, int idserie, string imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = id;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
                
            }
            

        }

        public async Task UpdateSeriePersonajeAsync(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/updatepersonajeserie/" + idpersonaje + "/" + idserie;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);

            }
        }

        #endregion
    }
}