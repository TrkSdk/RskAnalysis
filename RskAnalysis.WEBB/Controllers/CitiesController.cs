using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.WEBB.Services.CitiesSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class CitiesController : Controller
    {
        //private readonly AppDbContext _context;
        
        private readonly CitiesWServices _citiesWServices;

        public CitiesController(CitiesWServices citiesWServices)
        {
            //_context = context;
            _citiesWServices = citiesWServices;
        }
        // GET: Cities
        public async Task<IActionResult> Index()
        {
            var res = await _citiesWServices.GetCitiesAsync() as IEnumerable<Cities>;

            return View(res);
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _citiesWServices.GetCityById((int)id);
            
            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityName")] Cities cities)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(cities);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            var res = await _citiesWServices.AddCity(cities);
            cities = null;
            
            return RedirectToAction("Index");
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int id)  //int? id
        {
            if (id == null)
            {
                return NotFound();
            }
            var city = await _citiesWServices.GetCityById(id);
            
            
            return View(city);
           
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId","CityName")] Cities cities)
        {
            if (id != cities.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var res = await _citiesWServices.UpdateCity(cities);
                return RedirectToAction(nameof(Index));
            }
            return View(cities);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var city = await _citiesWServices.GetCityById(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("CityId", "CityName")] Cities city)
        {
            _citiesWServices.DeleteCity(city);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
