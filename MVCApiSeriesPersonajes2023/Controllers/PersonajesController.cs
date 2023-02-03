using Microsoft.AspNetCore.Mvc;
using MVCApiSeriesPersonajes2023.Models;
using MVCApiSeriesPersonajes2023.Services;

namespace MVCApiSeriesPersonajes2023.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceSeries service;
        public PersonajesController(ServiceSeries series)
        {
            this.service = series;
        }

        public async Task<IActionResult> PersonajesSerie(int idserie)
        {
            List<Personaje> personajes = await this.service.FindPersonajesSerieAsync(idserie);
            return View(personajes);

        }

        public async Task<IActionResult> CreatePersonaje()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(Personaje personaje)
        {
            await this.service.CreatePersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.IdSerie, personaje.Imagen);
            return RedirectToAction("PersonajesSerie", new { idserie = personaje.IdSerie });
        }

        
        public async Task<IActionResult> UpdatePersonajeSerie()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["PERSONAJES"] = personajes;
            ViewData["SERIES"] = series;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdatePersonajeSerie(int idpersonaje, int idserie)
        {
            await this.service.UpdateSeriePersonajeAsync(idpersonaje, idserie);
            return RedirectToAction("PersonajeSerie", new {idserie = idserie});
        }
    }
}
