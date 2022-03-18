using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPracticaAzureDSC.Services;
using MvcPracticaAzureDSC.Models;

namespace MvcPracticaAzureDSC.Controllers
{
    public class SeriesServerController : Controller
    {
        private ServiceSeries service;
        public SeriesServerController(ServiceSeries service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
          List<Serie> series = 
                await this.service.GetSeriesAsync();
            return View(series);
        }

        public async Task<IActionResult> Detalles(int idserie)
        {
            Serie serie =
                await this.service.FindSerieAsync(idserie);
            return View(serie);
        }

        public IActionResult InsertarSerie()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertarSerie(Serie serie)
        {
            await this.service.InsertarSerie
                (serie.Nombre, serie.Imagen, (float)serie.Puntuacion, serie.Anyo);
            return RedirectToAction("Index", "SeriesServer");
                
        }
        public async Task<IActionResult> EditarSerie(int idserie)
        {
            Serie serie =
                await this.service.FindSerieAsync(idserie);
            return View(serie);
        }
        [HttpPost]
        public async Task<IActionResult> EditarSerie(Serie serie)
        {
            await this.service.EditarSerie
                (serie.IdSerie,serie.Nombre, serie.Imagen, (float)serie.Puntuacion, serie.Anyo);
            return RedirectToAction("Index", "SeriesServer");

        }

        public async Task<IActionResult> DeleteSerie(int idserie)
        {
            await this.service.DeleteSerie(idserie);
            return RedirectToAction("Index", "SeriesServer");
        }
        public IActionResult PersonajesSerie()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PersonajesSerie(int idserie, int idpersonaje) {
            Personaje personaje =
               await this.service.GetPersonajeSerie(idserie, idpersonaje);
            return View(personaje);
               }



    }
}
