using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MvcPracticaAzureDSC.Models;
using Newtonsoft.Json;

namespace MvcPracticaAzureDSC.Services
{
    public class ServiceSeries
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceSeries(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
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

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }
        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            string request = "/api/Personajes/" + idpersonaje;
            Personaje personaje =
                await this.CallApiAsync<Personaje>(request);
            return personaje;
        }
        public async Task<Personaje> GetPersonajeSerie(int idserie, int idpersonaje)
        {
            string request = "/api/Personajes/PersonajeSerie" + idserie + "/" + idpersonaje;
            Personaje personaje =
                await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task InsertarPersonajes(string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";

                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = 1;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;

                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            
            
            }
        }
        

        public async Task EditarPersonajes(int idpersonaje, string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";

                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje =
                    await this.FindPersonajeAsync(idpersonaje);
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;

                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);

            }
        }

        public async Task DeletePersonajes(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + idpersonaje;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = 
                    await client.DeleteAsync(request);
            }
        }


        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series =
                await this.CallApiAsync<List<Serie>>(request);
            return series;
        }
        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie =
                await this.CallApiAsync<Serie>(request);
            return serie;
        }
     
        public async Task<List<Personaje>> GetPersonajesSeries(int idserie)
        {
            string request = "/api/series/PersonajesSerie/" + idserie;
            List<Personaje> series =
                await this.CallApiAsync<List<Personaje>>(request);
            return series;
        }


        public async Task InsertarSerie(string nombre, string imagen, float puntuacion, int anyo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series";

                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.IdSerie = 1;
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Anyo = anyo;

                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task EditarSerie(int idserie, string nombre, string imagen, float puntuacion, int anyo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series";

                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie =
                    await this.FindSerieAsync(idserie);
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion =puntuacion;
                serie.Anyo = anyo;

                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);

            }
        }
        public async Task DeleteSerie(int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series/" + idserie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }






    }
}
