using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.DATA.Repository.CitiesRepo;
using System.Diagnostics.Contracts;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsRepository _contractsRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContractsController( IContractsRepository contractsRepository,AppDbContext context, IMapper mapper)
        {
            _contractsRepository=contractsRepository;
            _context=context;
            _mapper=mapper;
        }

        [HttpGet, Route("ContractsList")]
        public async Task<IActionResult> GetContractsList()
        {
            var contrList = await _contractsRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ContractsDto>>(contrList));
        }

        [HttpGet, Route("Contracts/{id}")]
        public async Task<IActionResult> GetContractsById(int id)
        {
            var contr = await _contractsRepository.GetByIdAsync(id);

            return Ok(_mapper.Map<IEnumerable<ContractsDto>>(contr));

        }

        [HttpPost, Route("AddContract/{Contract}")]
        public IActionResult ContractAdd(Contracts contract)
        {
            //usrDto.Id = Guid.NewGuid();
            var contr = _contractsRepository.AddAsync(contract);

            return Ok(_mapper.Map<IEnumerable<ContractsDto>>(contr));

        }

        [HttpPut, Route("UpdateContract/{Contract}")]
        public IActionResult BusinessesUpdate(Contracts contract)
        {
            //usrDto.Id = Guid.NewGuid();
            var contr = _contractsRepository.Update(contract);

            return NoContent();

        }

        [HttpDelete, Route("DeleteContract/{Contract}")]
        public IActionResult ContractDelete(Contracts contract)
        {
            //usrDto.Id = Guid.NewGuid();


            _contractsRepository.Remove(contract);
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
