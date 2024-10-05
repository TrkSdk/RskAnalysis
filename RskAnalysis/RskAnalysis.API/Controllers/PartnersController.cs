using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.IntServices.IntPartnersServ;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.DATA.Repository.ContractsRepo;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnersService _partnersService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PartnersController(IPartnersService partnersService, IMapper mapper)
        {
            _partnersService= partnersService;
            //_context = context;
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
            var busList = await _partnersService.GetPartnersWithBussinessAndCity();
            return Ok(busList);
        }

        [HttpGet, Route("PartnersId/{id}")]
        public async Task<IActionResult> GetPartnersById(int id)
        {
            var part = await _partnersService.GetByIdAsync(id);
            //part.Sector = null;
            //part.City = null;
           
            
            return Ok(part);

        }

        [HttpGet, Route("PartnersIDWithBussinessAndCity/{id}")]
        public async Task<IActionResult> GetPartnersIDWithBussinessAndCity(int id)
        {
            var part = await _partnersService.GetPartnersByIdWithBussinessAndCity(id);

            return Ok(part);

        }

        [HttpPost, Route("AddPartner/{Partner}")]
        public IActionResult ContractAdd(Partners partner)
        {
            partner.Business = null;
            partner.City = null;
            
            var part = _partnersService.AddAsync(partner);

            //return CreatedAtAction(nameof(GetPartnersList), partner);
            return Ok(part);
        }

        [HttpPut, Route("UpdatePartner/{Partner}")]
        public IActionResult BusinessesUpdate(Partners partner)
        {
            //usrDto.Id = Guid.NewGuid();
            var part = _partnersService.Update(partner);

            return NoContent();

        }

        [HttpDelete, Route("DeletePartner/{Partner}")]
        public IActionResult PartnerDelete(Partners partner)
        {
            //usrDto.Id = Guid.NewGuid();


            _partnersService.Remove(partner);
            return NoContent();



            //if (orDet.Result.Count()>0)
            //{

            //}
            //else
            //{
            //    ErrorDto errorDto = new ErrorDto();
            //    errorDto.Status = 404;//NotFound hata kodu.

            //    errorDto.Errors.Add($" Guncellenecek kayit bulunamadi.");
            //    return new NotFoundObjectResult(errorDto);
            //}

        }
    }
}
