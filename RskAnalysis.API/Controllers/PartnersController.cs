using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.DATA.Repository.ContractsRepo;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnersRepository _partnersRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PartnersController(IPartnersRepository partnersRepository, AppDbContext context, IMapper mapper)
        {
            _partnersRepository = partnersRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("PartnersList")]
        public async Task<IActionResult> GetPartnersList()
        {
            var partList = await _partnersRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PartnersDto>>(partList));
        }

        [HttpGet, Route("Partners/{id}")]
        public async Task<IActionResult> GetPartnersById(int id)
        {
            var part = await _partnersRepository.GetByIdAsync(id);

            return Ok(_mapper.Map<IEnumerable<PartnersDto>>(part));

        }

        [HttpPost, Route("AddPartner/{Partner}")]
        public IActionResult ContractAdd(Partners partner)
        {
            //usrDto.Id = Guid.NewGuid();
            var part = _partnersRepository.AddAsync(partner);

            return Ok(_mapper.Map<IEnumerable<PartnersDto>>(part));

        }

        [HttpPut, Route("UpdatePartner/{Partner}")]
        public IActionResult BusinessesUpdate(Partners partner)
        {
            //usrDto.Id = Guid.NewGuid();
            var part = _partnersRepository.Update(partner);

            return NoContent();

        }

        [HttpDelete, Route("DeletePartner/{Partner}")]
        public IActionResult PartnerDelete(Partners partner)
        {
            //usrDto.Id = Guid.NewGuid();


            _partnersRepository.Remove(partner);
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
