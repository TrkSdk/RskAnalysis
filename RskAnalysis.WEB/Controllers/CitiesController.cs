using Microsoft.AspNetCore.Mvc.RazorPages;
using RskAnalysis.CORE.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.WEB.Services.CitiesSer;
using Ct = RskAnalysis.WEB.Models.CitiesW;
using RskAnalysis.WEB.Models;

namespace RskAnalysis.WEB.Controllers
{
    public class CitiesController : Controller
    {

      
        private readonly CitiesWServices _citiesWServices;

        public CitiesController(CitiesWServices citiesWServices)
        {
            _citiesWServices = citiesWServices;
        }

        public async Task<IActionResult> Index()
        {
            //var response = await _httpClient.GetAsync("https://localhost:7009/api/CitiesControllers/GetCitiesList");


            var res = await _citiesWServices.GetCitiesAsync();

            List<Ct> ct = res.Cast<Ct>().ToList();
            


            return View(ct);
        }
    }
}
