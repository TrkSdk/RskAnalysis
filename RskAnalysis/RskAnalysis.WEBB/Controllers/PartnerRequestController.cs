using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RskAnalysis.CORE.Models;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.PartnerRiskSer;
using RskAnalysis.WEBB.Services.PartnersSer;
using RskAnalysis.WEBB.Services.SectorsSer;

namespace RskAnalysis.WEBB.Controllers
{
    public class PartnerRequestController : Controller
    {
        private readonly BusinessesWServices _businessesWServices;
        private readonly SectorsWServices _sectorsWServices;
        private readonly PartnersWServices _partnersWServices;
        private readonly PartnerRequestWServices _partnerRequestWServices;

        public PartnerRequestController(BusinessesWServices businessesWServices, SectorsWServices sectorsWServices, PartnersWServices partnersWServices, PartnerRequestWServices partnerRequestWServices)
        {
            _businessesWServices = businessesWServices;
            _sectorsWServices = sectorsWServices;
            _partnersWServices = partnersWServices;
            _partnerRequestWServices = partnerRequestWServices;
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
        public async Task<IActionResult> Create(PartnerRequest partnerRequest)
        {
            var ress = await _partnersWServices.GetPartnerById(partnerRequest.PartnerId);

            //partnerRequest.PartnerName = ress.PartnerName;

            //ModelState.Remove(partnerRequest.PartnerName);

            if (ModelState.IsValid)
            {
                var res = await _partnerRequestWServices.TakePartnerRequest(partnerRequest);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));

        }

    }
}
