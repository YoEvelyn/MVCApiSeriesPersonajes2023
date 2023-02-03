using Microsoft.AspNetCore.Mvc;
using MVCApiSeriesPersonajes2023.Models;
using MVCApiSeriesPersonajes2023.Services;

namespace MVCApiSeriesPersonajes2023.ViewComponents
{
    public class MenuSeriesViewComponent: ViewComponent
    {
        private ServiceSeries service;

        public MenuSeriesViewComponent(ServiceSeries service)
        {
            this.service = service;
        }

        //EL METODO InvokeAsync() ES EL ENCARGAO DE ENVIAR UN MODEL AL LAYOUT
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }


    }
}
