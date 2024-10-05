using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessesService _businessService;
        private readonly IMapper _mapper;

        public BusinessesController(IBusinessesService businessService, IMapper mapper)
        {
            _businessService=businessService;
            _mapper=mapper;
        }

        [HttpGet, Route("BusinessesList")]
        public async Task<IActionResult> GetBussinessesList()
        {
            var busList = await _businessService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BusinessesDto>>(busList));
        }

        [HttpGet, Route("BusinessesWithSector")]
        public async Task<IActionResult> GetBussinessesWithSector()
        {
            var busList = await _businessService.GetBussinessWithSectors();
            return Ok(busList);
        }

        [HttpGet, Route("BusinessesID/{id}")]
        public async Task<IActionResult> GetBusinessesById(int id)
        {
            var buss = await _businessService.GetByIdAsync(id);

            return Ok(buss);

        }

        [HttpGet, Route("BusinessesIDWithSector/{id}")]
        public async Task<IActionResult> GetBusinessesByIdWithSector(int id)
        {
            var bus = await _businessService.GetBussinessByIdWithSectors(id);

            return Ok(bus);

        }

        [HttpPost, Route("AddBusinesses/{business}")]
        public IActionResult BusinessesAdd(Businesses business)
        {
            business.Sector = null;
            var bus = _businessService.AddAsync(business);

            return Ok(bus);
        }

        [HttpPut, Route("UpdateBusinesses/{businesessId}")]
        public IActionResult BusinessesUpdate(int businesessId,[FromBody]Businesses business)
        {
            // Eğer ID'ler uyuşmazsa hata dönebiliriz
            if (businesessId != business.BusinessId)
            {
                return BadRequest("Business ID uyusmadi.");
            }
            //usrDto.Id = Guid.NewGuid();
            var bus = _businessService.Update(business);
            if (bus == null)
            {
                return NotFound("Business not found.");
            }

            return Ok();

        }

        [HttpDelete, Route("DeleteBusiness/{Business}")]
        public IActionResult BusinessDelete(Businesses business)
        {

                _businessService.Remove(business);
                return Ok();
            
        }

    }
}
