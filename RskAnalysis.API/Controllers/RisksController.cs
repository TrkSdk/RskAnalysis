using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.IntRepository.IntRisksRepository;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.DATA.Repository.PartnersRepo;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly IRisksRepository _risksRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RisksController(IRisksRepository risksRepository, AppDbContext context, IMapper mapper)
        {
            _risksRepository = risksRepository;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet, Route("RisksList")]
        public async Task<IActionResult> GetRisksList()
        {
            var riskList = await _risksRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RisksDto>>(riskList));
        }

        [HttpGet, Route("Risks/{id}")]
        public async Task<IActionResult> GetRisksById(int id)
        {
            var rsk = await _risksRepository.GetByIdAsync(id);

            return Ok(_mapper.Map<IEnumerable<RisksDto>>(rsk));

        }

        [HttpPost, Route("AddRisk/{Risk}")]
        public IActionResult RiskAdd(Risks risk)
        {
            //usrDto.Id = Guid.NewGuid();
            var rsk = _risksRepository.AddAsync(risk);

            return Ok(_mapper.Map<IEnumerable<RisksDto>>(rsk));

        }

        [HttpPut, Route("UpdateRisk/{Risk}")]
        public IActionResult RiskUpdate(Risks risk)
        {
            //usrDto.Id = Guid.NewGuid();
            var rsk = _risksRepository.Update(risk);

            return NoContent();

        }

        [HttpDelete, Route("DeleteRisk/{Risk}")]
        public IActionResult RiskDelete(Risks risk)
        {
            //usrDto.Id = Guid.NewGuid();


            _risksRepository.Remove(risk);
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
