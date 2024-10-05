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
        //private readonly AppDbContext _context;

        private readonly PartnersWServices _partnersWServices;
        private readonly CitiesWServices _citiesWServices;
        private readonly BusinessesWServices _businessesWServices;
        private readonly SectorsWServices _sectorsWServices;


        public PartnersController(PartnersWServices partnersWServices, CitiesWServices citiesWServices, BusinessesWServices businessesWServices, SectorsWServices sectorsWServices)
        {
            _partnersWServices = partnersWServices;
            _citiesWServices = citiesWServices;
            _businessesWServices = businessesWServices;
            _sectorsWServices = sectorsWServices;
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            var res = await _partnersWServices.GetPartnersWithBussinessAndCity() as IEnumerable<Partners>;

            //var rrr = res.ToList();

            return View(res);

        }

        // GET: Partners/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _partnersWServices.GetPartnerById(id);

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
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
        public async Task<IActionResult> Create([Bind("PartnerId,BusinessId,PartnerName,ContactPerson,CityId,ContactEMail,RiskFactor,CreatedDate")] Partners partners)
        {
            ModelState.Remove("Business.BusinessName");
            ModelState.Remove("Business.BusinessDescription");
            ModelState.Remove("Business.Sector.SectorName");
            ModelState.Remove("Business.Sector.SectorDescription");
            ModelState.Remove("City.CityName");

            if (ModelState.IsValid)
            {
                //partners.BusinessId = 1;
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
            if (id <= null)
            {
                return NotFound();
            }

            var res = await _partnersWServices.GetPartnerByIdWithBussinessAndCity(id);

            if (res == null || res.Count == 0)
            {
                return NotFound();
            }


            var part = new List<Businesses> { res[0].Business };
            ViewData["BusinessId"] = new SelectList(part, "BusinessId", "BusinessName", res[0].Business.BusinessId);


            var cty = new List<Cities> { res[0].City };
            ViewData["CityId"] = new SelectList(cty, "CityId", "CityName", res[0].City.CityId);


            return View(res[0]);
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

            var part = await _partnersWServices.GetPartnerById(partners.PartnerId);
            part.Business = null;
            part.City= null;
            
            part.PartnerName = partners.PartnerName;
            part.BusinessId = partners.BusinessId;
            part.ContactPerson = partners.ContactPerson;
            part.CityId= partners.CityId;
            part.ContactEMail=partners.ContactEMail;
            part.ContactPerson= partners.ContactPerson;
            part.RiskFactor=partners.RiskFactor;
            part.CreatedDate= DateTime.Now;

            
            var upt = await _partnersWServices.UpdatePartner(part);
            return RedirectToAction(nameof(Index));
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _partnersWServices.GetPartnerByIdWithBussinessAndCity(id);
            if (part == null)
            {
                return NotFound();
            }

            var buss = await _businessesWServices.GetBusinessById(part[0].BusinessId);
            part[0].Business.BusinessId = buss.BusinessId;
            part[0].Business.SectorId = buss.SectorId;
            part[0].Business.BusinessName = buss.BusinessName;
            part[0].Business.BusinessDescription = buss.BusinessDescription;
            part[0].Business.RiskFactor = buss.RiskFactor;
            part[0].Business.CreatedDate = buss.CreatedDate;

            var sect = await _sectorsWServices.GetSectorById(part[0].BusinessId);
            part[0].Business.Sector.SectorId = sect.SectorId;
            part[0].Business.Sector.SectorName = sect.SectorName;
            part[0].Business.Sector.SectorDescription = sect.SectorDescription;
            part[0].Business.Sector.CreatedDate = sect.CreatedDate;


            var cty = await _citiesWServices.GetCityById(part[0].CityId);
            part[0].City.CityId = cty.CityId;
            part[0].City.CityName = cty.CityName;

            return View(part[0]);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Partners part)
        {
            _partnersWServices.DeletePartner(part);

            return RedirectToAction(nameof(Index));
        }

        //private bool PartnersExists(int id)
        //{
        //    return (_context.Partners?.Any(e => e.PartnerId == id)).GetValueOrDefault();
        //}
    }
}
