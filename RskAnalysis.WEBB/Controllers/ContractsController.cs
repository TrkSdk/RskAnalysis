using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RskAnalysis.CORE.Models;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.CitiesSer;
using RskAnalysis.WEBB.Services.ContractsSer;
using RskAnalysis.WEBB.Services.PartnersSer;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class ContractsController : Controller
    {
        private readonly PartnersWServices _partnersWServices;
        private readonly BusinessesWServices _businessesWServices;
        private readonly SectorsWServices _sectorssWServices;
        private readonly CitiesWServices _citiesWServices;
        private readonly ContractsWServices _contractsWServices;

        public ContractsController(ContractsWServices contractsWServices, PartnersWServices partnersWServices, BusinessesWServices businessesWServices, SectorsWServices sectorsWServices, CitiesWServices citiesWServices)
        {
            _contractsWServices = contractsWServices;
            _businessesWServices = businessesWServices;
            _sectorssWServices = sectorsWServices;
            _citiesWServices = citiesWServices;
            _partnersWServices = partnersWServices;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var res = await _contractsWServices.GetContractWithPart() as IEnumerable<Contracts>;

            return View(res);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _contractsWServices.GetContractById(id);

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");

            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contracts contracts)
        {
            ModelState.Remove("Partner.PartnerName");
            ModelState.Remove("Partner.ContactPerson");
            ModelState.Remove("Partner.ContactEmail");
            ModelState.Remove("Partner.StartDate");
            ModelState.Remove("Partner.EndDate");
            ModelState.Remove("Partner.RiskFactor");
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
                contracts.IsRejected = false;
                var res = await _contractsWServices.AddContract(contracts);
                return RedirectToAction(nameof(Index));
            }

            ViewData["PartnerId"] = new SelectList(await _partnersWServices.GetPartnersAsync(), "PartnerId", "PartnerName");

            return View(contracts);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var res = await _contractsWServices.GetContractByIdWithPartner(id);
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }

            var secc = new List<Partners> { res[0].Partner };

            ViewData["PartnerId"] = new SelectList(secc, "PartnerId", "PartnerName", res[0].Partner.PartnerId);

            return View(res[0]);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Contracts contracts)
        {

            //[Bind("ContractId,Amount,BusinessId, PartnerId,ContractName,StartDate,EndDate,RiskFactor,CreatedDate")] 
            
            if (id != contracts.ContractId)
            {
                return NotFound();
            }

            var cntrct = await _contractsWServices.GetContractById(contracts.ContractId);

            cntrct.Partner = null;

            cntrct.ContractId = contracts.ContractId;
            cntrct.ContractName = contracts.ContractName;
            cntrct.StartDate = contracts.StartDate;
            cntrct.EndDate = contracts.EndDate;
            cntrct.StartDate = contracts.StartDate;
            cntrct.Amount = contracts.Amount;
            cntrct.RiskFactor = contracts.RiskFactor;
            cntrct.CreatedDate = contracts.CreatedDate;
            cntrct.IsRejected = false;

            var upt = await _contractsWServices.UpdateContract(cntrct);
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cntrct = await _contractsWServices.GetContractByIdWithPartner(id);
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

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Contracts cntrct)
        {

                _contractsWServices.DeleteContract(cntrct);

                return RedirectToAction(nameof(Index));
        }
    }
}
