using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntServices.IntPartnersServ;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnersService _partnersService;
        private readonly IMapper _mapper;

        public PartnersController(IPartnersService partnersService, IMapper mapper)
        {
            _partnersService= partnersService;
            _mapper = mapper;
        }

        [HttpGet, Route("PartnersList")]
        public async Task<IActionResult> GetPartnersList()
        {
            var partList = await _partnersService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PartnersDto>>(partList));
        }

        [HttpGet, Route("PartnersWithBussinessAndCity")]
        public async Task<IActionResult> GetPartnersWithBussinessAndCity()
        {
            var partList = await _partnersService.GetPartnersWithBussinessAndCity();
            return Ok(partList);
        }

        [HttpGet, Route("PartnerID/{id}")]
        public async Task<IActionResult> GetPartnersById(int id)
        {
            var part = await _partnersService.GetByIdAsync(id);

            return Ok(part);

        }

        [HttpGet, Route("PartnersIDWithBussinessAndCity/{id}")]
        public async Task<IActionResult> GetPartnersIDWithBussinessAndCity(int id)
        {
            var part = await _partnersService.GetPartnersByIdWithBussinessAndCity(id);

            return Ok(part);

        }

        [HttpPost, Route("AddPartner/{Partner}")]
        public IActionResult AddPartner(Partners partner)
        {
            partner.Business = null;
            partner.City = null;
            
            var part = _partnersService.AddAsync(partner);

            return Ok(part);
        }

        [HttpPut, Route("UpdatePartner/{partnerId}")]
        public IActionResult UpdatePartner(int partnerId, [FromBody] Partners partner)
        {
            // Eğer ID'ler uyuşmazsa hata dönebiliriz
            if (partnerId != partner.PartnerId)
            {
                return BadRequest("Partner ID uyusmadi.");
            }
            //usrDto.Id = Guid.NewGuid();
            var part = _partnersService.Update(partner);
            if (part == null)
            {
                return NotFound("Partner not found.");
            }

            return Ok();


        }

        [HttpDelete, Route("DeletePartner/{partner}")]
        public IActionResult PartnerDelete(Partners partner)
        {

                _partnersService.Remove(partner);
                return Ok();
        }
    }
}
