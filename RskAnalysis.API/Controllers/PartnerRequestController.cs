using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.CORE.IntServices.IntContractsServ;
using RskAnalysis.CORE.IntServices.IntPartnerRequestServ;
using RskAnalysis.CORE.IntServices.IntRejectedContractsServ;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerRequestController : ControllerBase
    {
        private readonly IPartnerRequestService _partnerRequestService;
        private readonly IRejectedContractsService _rejectedcontractsService;
        private readonly IContractsService _contractsService;
        private readonly IMapper _mapper;

        public PartnerRequestController(IPartnerRequestService partnerRequestService, IContractsService contractsService, IRejectedContractsService rejectedcontractsService, IMapper mapper)
        {
            _partnerRequestService = partnerRequestService;
            _contractsService = contractsService;
            _rejectedcontractsService = rejectedcontractsService;
            _mapper = mapper;
        }

        [HttpPost, Route("TakePartnerRequest/{contract}")]
        public async Task<IActionResult> TakePartnerRequest(Contracts contract)
        {
            
            var cntrct = await _partnerRequestService.TakePartnerRequest(contract); //burada contract doğru

            return Ok(cntrct);
        }                        
    }
}
