using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.CitiesSer;
using RskAnalysis.WEBB.Services.PartnersSer;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class PartnersController : Controller
    {
        private readonly AppDbContext _context;

        private readonly PartnersWServices _partnersWServices;
        private readonly CitiesWServices _citiesWServices;
        private readonly BusinessesWServices _businessesWServices;


        public PartnersController(PartnersWServices partnersWServices, CitiesWServices citiesWServices, BusinessesWServices businessesWServices)
        {
            _partnersWServices = partnersWServices;
            _citiesWServices = citiesWServices;
            _businessesWServices = businessesWServices;
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            var res = await _partnersWServices.GetPartnersWithBussinessAndCity() as IEnumerable<Partners>;

            //var rrr = res.ToList();

            return View(res);

        }

        // GET: Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _partnersWServices.GetPartnerByIdWithBussinessAndCity((int)id) ;
            

            if (res == null)
            {
                return NotFound();
            }

            return View(res[0]);
        }

        // GET: Partners/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_citiesWServices.GetCitiesAsync().Result, "CityId", "CityName");

            ViewData["BusinessId"] = new SelectList(_businessesWServices.GetBusinessAsync().Result, "BusinessId", "BusinessName");
            
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Partners partners)
        {
            //[Bind("PartnerId,BusinessId,PartnerName,ContactPerson,CityId,ContactEMail,RiskFactor,CreatedDate")]
            if (ModelState.IsValid)
            {
                var part = await _partnersWServices.AddPartner(partners);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessId"] = new SelectList(_businessesWServices.GetBusinessAsync().Result, "BusinessId", "BusinessName");
            ViewData["CityId"] = new SelectList(_citiesWServices.GetCitiesAsync().Result, "CityId", "CityName");
            return View(partners);
        }

        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var buss = await _businessesWServices.GetBusinessById(id);

            return View(buss);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerId,BusinessId,PartnerName,ContactPerson,CityId,ContactEMail,RiskFactor,CreatedDate")] Partners partners)
        {
            if (id != partners.PartnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(partners);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!PartnersExists(partners.PartnerId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                var res = await _partnersWServices.UpdatePartner(partners);
                return RedirectToAction(nameof(Index));
            }
            return View(partners);
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var partner = await _partnersWServices.GetPartnerById(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PartnerId,BusinessId,PartnerName,ContactPerson,CityId,ContactEMail,RiskFactor,CreatedDate")] Partners partner)
        {
            _partnersWServices.DeletePartner(partner);

            return RedirectToAction(nameof(Index));
        }

        private bool PartnersExists(int id)
        {
            return (_context.Partners?.Any(e => e.PartnerId == id)).GetValueOrDefault();
        }
    }
}
