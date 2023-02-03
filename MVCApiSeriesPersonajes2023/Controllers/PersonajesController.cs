using Microsoft.AspNetCore.Mvc;
using MVCApiSeriesPersonajes2023.Helpers;
using MVCApiSeriesPersonajes2023.Models;
using MVCApiSeriesPersonajes2023.Services;

namespace MVCApiSeriesPersonajes2023.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceSeries service;
        private HelperPathProvider helperPath;
        public PersonajesController(ServiceSeries series, HelperPathProvider helperPath)
        {
            this.service = series;
            this.helperPath = helperPath;
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
        public async Task<IActionResult> CreatePersonaje(Personaje personaje, IFormFile fichero)
        {
            //subir el fichero al servidor
            string fileName = fichero.FileName;
            string path = this.helperPath.GetMapPath(Folders.Imagenes, fileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            //Guardamos en el api la url del servidor con la imagen
            string folder = this.helperPath.GetNameFolder(Folders.Imagenes);
            personaje.Imagen = this.helperPath.GetWebHostUrl() + folder + "/" + fileName;
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
