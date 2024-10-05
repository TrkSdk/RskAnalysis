using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.CitiesSer;
using RskAnalysis.WEBB.Services.ContractsSer;
using RskAnalysis.WEBB.Services.PartnersSer;
using RskAnalysis.WEBB.Services.RejectedContractsSer;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class RejectedContractsController : Controller
    {
        private readonly PartnersWServices _partnersWServices;
        private readonly BusinessesWServices _businessesWServices;
        private readonly SectorsWServices _sectorssWServices;
        private readonly CitiesWServices _citiesWServices;
        private readonly RejectedContractsWServices _rejectedcontractsWServices;

        public RejectedContractsController(RejectedContractsWServices rejectedcontractsWServices, PartnersWServices partnersWServices, BusinessesWServices businessesWServices, SectorsWServices sectorsWServices, CitiesWServices citiesWServices)
        {
            _rejectedcontractsWServices = rejectedcontractsWServices;
            _businessesWServices = businessesWServices;
            _sectorssWServices = sectorsWServices;
            _citiesWServices = citiesWServices;
            _partnersWServices = partnersWServices;
        }



        // GET: RejectedContracts
        public async Task<IActionResult> Index()
        {
            var res = await _rejectedcontractsWServices.GetRejectedContractWithPart() as IEnumerable<Contracts>;

            return View(res);
        }

        // GET: RejectedContracts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _rejectedcontractsWServices.GetRejectedContractById(id);

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // GET: RejectedContracts/Create
        public IActionResult Create()
        {
            ViewData["BusinessId"] = new SelectList(_businessesWServices.GetBusinessAsync().Result, "BusinessId", "PartnerName");
            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");
            
            return View();
        }

        // POST: RejectedContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,Amount,BusinessId,PartnerId,ContractName,StartDate,RiskFactor,EndDate,CreatedDate")] Contracts rejectedContracts)
        {
            ModelState.Remove("Partner.PartnerName");
            ModelState.Remove("Partner.ContactPerson");
            ModelState.Remove("Partner.ContactEmail");
            ModelState.Remove("Partner.StartDate");
            ModelState.Remove("Partner.EndDate");
            ModelState.Remove("Partner.RiskFactor");
            ModelState.Remove("Partner.Business.BusinessName");
            ModelState.Remove("Partner.Business.BusinessDescription");
            ModelState.Remove("Partner.Business.RiskFactor");
            ModelState.Remove("Partner.Business.Sector.SectorName");
            ModelState.Remove("Partner.Business.Sector.SectorDescription");
            ModelState.Remove("Partner.City.CityName");
            ModelState.Remove("Business.BusinessName");
            ModelState.Remove("Business.BusinessDescription");
            ModelState.Remove("Business.Sector.SectorName");
            ModelState.Remove("Business.Sector.SectorDescription");

            if (ModelState.IsValid)
            {
                rejectedContracts.IsRejected= true;
                var res = await _rejectedcontractsWServices.AddRejectedContract(rejectedContracts);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessId"] = new SelectList(_businessesWServices.GetBusinessAsync().Result, "BusinessId", "PartnerName");
            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");

            return View(rejectedContracts);
        }

        // GET: RejectedContracts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var res = await _rejectedcontractsWServices.GetRejectedContractByIdWithPartner(id);
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }

            var secc = new List<Partners> { res[0].Partner };

            ViewData["BusinessId"] = new SelectList(_businessesWServices.GetBusinessAsync().Result, "BusinessId", "PartnerName");
            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");

            return View(res[0]);

        }

        // POST: RejectedContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contracts rejectedContracts)
        {
            if (id != rejectedContracts.ContractId)
            {
                return NotFound();
            }

            var cntrct = await _rejectedcontractsWServices.GetRejectedContractById(rejectedContracts.ContractId);

            cntrct.Partner = null;

            cntrct.ContractId = rejectedContracts.ContractId;
            cntrct.ContractName = rejectedContracts.ContractName;
            cntrct.StartDate = rejectedContracts.StartDate;
            cntrct.EndDate = rejectedContracts.EndDate;
            cntrct.StartDate = rejectedContracts.StartDate;
            cntrct.Amount = rejectedContracts.Amount;
            cntrct.RiskFactor = rejectedContracts.RiskFactor;
            cntrct.CreatedDate = rejectedContracts.CreatedDate;
            cntrct.IsRejected = true;

            var upt = await _rejectedcontractsWServices.UpdateRejectedContract(cntrct);
            return RedirectToAction(nameof(Index));
        }

        // GET: RejectedContracts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cntrct = await _rejectedcontractsWServices.GetRejectedContractByIdWithPartner(id);
            if (cntrct == null)
            {
                return NotFound();
            }



            //var part = await _partnersWServices.GetPartnerById(cntrct[0].PartnerId);

            //cntrct[0].Partner.PartnerId = part.PartnerId;
            //cntrct[0].Partner.PartnerId = part.PartnerId;
            //cntrct[0].Partner.BusinessId = part.BusinessId;
            //cntrct[0].Partner.ContactPerson = part.ContactPerson;
            //cntrct[0].Partner.ContactEMail = part.ContactEMail;
            //cntrct[0].Partner.CityId = part.CityId;
            //cntrct[0].Partner.RiskFactor = part.RiskFactor;
            //cntrct[0].Partner.CreatedDate = part.CreatedDate;



            var buss = await _businessesWServices.GetBusinessById(cntrct[0].Partner.BusinessId);
            if (buss == null)
            {
                return NotFound();
            }
            cntrct[0].Partner.CityId = cntrct[0].Partner.CityId;

            cntrct[0].Partner.Business.BusinessId = buss.BusinessId;
            cntrct[0].Partner.Business.BusinessName = buss.BusinessName;
            cntrct[0].Partner.Business.BusinessDescription = buss.BusinessDescription;
            cntrct[0].Partner.Business.SectorId = buss.SectorId;
            cntrct[0].Partner.Business.CreatedDate = buss.CreatedDate;
            cntrct[0].Partner.Business.RiskFactor = buss.RiskFactor;



            var sect = await _sectorssWServices.GetSectorById(buss.SectorId);

            if (sect == null)
            {
                return NotFound();
            }
            cntrct[0].Partner.Business.Sector.SectorId = sect.SectorId;
            cntrct[0].Partner.Business.Sector.SectorName = sect.SectorName;
            cntrct[0].Partner.Business.Sector.SectorDescription = sect.SectorDescription;
            cntrct[0].Partner.Business.Sector.CreatedDate = sect.CreatedDate;




            var cty = await _citiesWServices.GetCityById(cntrct[0].Partner.CityId);

            if (cty == null)
            {
                return NotFound();
            }
            cntrct[0].Partner.City.CityId = cty.CityId;
            cntrct[0].Partner.City.CityName = cty.CityName;



            return View(cntrct[0]);
        }

        // POST: RejectedContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Contracts cntrct)
        {
            _rejectedcontractsWServices.DeleteRejectedContract(cntrct);

            return RedirectToAction(nameof(Index));
        }

    }
}
