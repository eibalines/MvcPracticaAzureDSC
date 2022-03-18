using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPracticaAzureDSC.Services;
using MvcPracticaAzureDSC.Models;

namespace MvcPracticaAzureDSC.Controllers
{
    public class PersonajesServerController : Controller
    {
        private ServiceSeries service;
        public PersonajesServerController(ServiceSeries service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes =
                  await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Detalles(int idpersonaje)
        {
            Personaje personaje =
                await this.service.FindPersonajeAsync(idpersonaje);
            return View(personaje);
        }

        public IActionResult InsertarPersonaje()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertarPersonaje(Personaje personaje)
        {
            await this.service.InsertarPersonajes
                (personaje.Nombre, personaje.Imagen, personaje.IdPersonaje);
            return RedirectToAction("Index", "PersonajesServer");

        }
        public async Task<IActionResult> EditarPersonaje(int idpersonaje)
        {
            Personaje personaje =
                await this.service.FindPersonajeAsync(idpersonaje);
            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> EditarPersonaje(Personaje personaje)
        {
            await this.service.EditarPersonajes
                (personaje.IdPersonaje, personaje.Nombre, personaje.Imagen, personaje.IdSerie);
            return RedirectToAction("Index", "PersonajesServer");

        }

        public async Task<IActionResult> DeletePersonaje(int idpersonaje)
        {
            await this.service.DeletePersonajes(idpersonaje);
            return RedirectToAction("Index", "PersonajesServer");
        }
 
        public async Task<IActionResult> PersonajesSeries(int idserie)
        {
            List<Personaje> personajes =
               await this.service.GetPersonajesSeries(idserie);
            return View(personajes);
        }
    }
}
