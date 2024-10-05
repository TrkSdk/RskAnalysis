using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RskAnalysis.CORE.Models;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.PartnerRiskSer;
using RskAnalysis.WEBB.Services.PartnersSer;
using RskAnalysis.WEBB.Services.SectorsSer;
using RskAnalysis.WEBB.Services.CitiesSer;
using RskAnalysis.WEBB.Services.ContractsSer;
using RskAnalysis.WEBB.Services.RejectedContractsSer;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.DATA;
using System.Diagnostics.Contracts;

namespace RskAnalysis.WEBB.Controllers
{

    public class PartnerRequestController : Controller
    {
        private readonly BusinessesWServices _businessesWServices;
        private readonly CitiesWServices _citiesWServices;
        private readonly SectorsWServices _sectorsWServices;
        private readonly PartnersWServices _partnersWServices;
        private readonly PartnerRequestWServices _partnerRequestWServices;
        private readonly ContractsWServices _contractsWServices;
        private readonly RejectedContractsWServices _rejectedcontractsWServices;

        public PartnerRequestController(BusinessesWServices businessesWServices, SectorsWServices sectorsWServices, CitiesWServices citiesWServices, PartnersWServices partnersWServices, PartnerRequestWServices partnerRequestWServices, ContractsWServices contractsWServices, RejectedContractsWServices rejectedcontractsWServices)
        {
            _businessesWServices = businessesWServices;
            _sectorsWServices = sectorsWServices;
            _citiesWServices = citiesWServices;
            _partnersWServices = partnersWServices;
            _partnerRequestWServices = partnerRequestWServices;
            _contractsWServices = contractsWServices;
            _rejectedcontractsWServices = rejectedcontractsWServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: PartnerRequestController
        public IActionResult Create()
        {

            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contracts contract) //contract doğru geliyot
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
                var cntrct = await _partnerRequestWServices.TakePartnerRequest(contract);   //1 buradan null dönüyor


                if (cntrct.IsRejected)
                {
                    var res = await _rejectedcontractsWServices.AddRejectedContract(cntrct);
                }
                else
                {
                    var res = await _contractsWServices.AddContract(cntrct);

                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PartnerId"] = new SelectList(await _partnersWServices.GetPartnersAsync(), "PartnerId", "PartnerName");
            
            return View(contract);


        }

        public IActionResult CreateContract()
        {
            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");

            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContract(Contracts contracts)
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
                var res = await _partnerRequestWServices.AddPartnerContract(contracts);
                return RedirectToAction(nameof(Index));
            }

            ViewData["PartnerId"] = new SelectList(await _partnersWServices.GetPartnersAsync(), "PartnerId", "PartnerName");

            return View(contracts);
        }



        // GET: RejectedContracts/Create
        public IActionResult CreateRejectedContract()
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
        public async Task<IActionResult> CreateRejectedContract([Bind("ContractId,Amount,BusinessId,PartnerId,ContractName,StartDate,EndDate,CreatedDate")] Contracts rejectedContracts)
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
                var res = await _partnerRequestWServices.AddRejectedPartnerContract(rejectedContracts);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessId"] = new SelectList(_businessesWServices.GetBusinessAsync().Result, "BusinessId", "PartnerName");
            ViewData["PartnerId"] = new SelectList(_partnersWServices.GetPartnersAsync().Result, "PartnerId", "PartnerName");

            return View(rejectedContracts);
        }


    }
}
