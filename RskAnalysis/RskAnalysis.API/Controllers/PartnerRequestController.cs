using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using RskAnalysis.CORE.IntServices.IntPartnerRequestServ;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerRequestController : ControllerBase
    {
        private readonly IPartnerRequestService _partnerRequestService;
        //private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PartnerRequestController(IPartnerRequestService partnerRequestService, IMapper mapper)
        {
            _partnerRequestService = partnerRequestService;
            _mapper = mapper;
        }

        [HttpPost, Route("TakePartnerRequest/{partnerRequest}")]
        public IActionResult TakePartnerRequest(PartnerRequest partnerRequest)
        {
            
            var bus = _partnerRequestService.TakePartnerRequest(partnerRequest);

            //return Ok(_mapper.Map<IEnumerable<BusinessesDto>>(bus));
            return Ok(bus);
        }
    }
}
