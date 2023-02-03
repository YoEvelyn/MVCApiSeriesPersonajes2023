using Microsoft.AspNetCore.Mvc;
using MVCApiSeriesPersonajes2023.Services;
using MVCApiSeriesPersonajes2023.Models;

namespace MVCApiSeriesPersonajes2023.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceSeries service;

        public SeriesController(ServiceSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        
        public async Task<IActionResult> Detalles(int idserie)
        {
            Serie serie = await this.service.FindSeriesAsync(idserie);
            return View(serie);
        } 
        
    }
}
