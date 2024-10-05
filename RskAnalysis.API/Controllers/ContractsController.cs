using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.CORE.IntServices.IntContractsServ;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _contractsService;
        private readonly IMapper _mapper;

        public ContractsController(IContractsService contractsService, AppDbContext context, IMapper mapper)
        {
            _contractsService= contractsService;
            _mapper=mapper;
        }

        [HttpGet, Route("ContractsList")]
        public async Task<IActionResult> GetContractsList()
        {
            var contrList = await _contractsService.GetAllAsync();
            contrList = contrList.Where(x => x.IsRejected == false);
            
            return Ok(_mapper.Map<IEnumerable<ContractsDto>>(contrList));
        }

        [HttpGet, Route("ContractsWithPartner")]
        public async Task<IActionResult> GetContractWithPartner()
        {
            var parList = await _contractsService.GetContractWithPartners();
            return Ok(parList);
        }


        [HttpGet, Route("ContractsID/{id}")]
        public async Task<IActionResult> GetContractById(int id)
        {
            var cntrct = await _contractsService.GetByIdAsync(id);

            return Ok(cntrct);

        }

        [HttpGet, Route("ContractsIDWithPartner/{id}")]
        public async Task<IActionResult> GetContractByIdWithPartner(int id)
        {
            var cntrct = await _contractsService.GetContractByIdWithPartners(id);

            return Ok(cntrct);

        }

        [HttpPost, Route("AddContracts/{Contract}")]
        public IActionResult ContractAdd(Contracts contract)
        {
            contract.Partner = null;
            contract.IsRejected= false;
            var cntrct = _contractsService.AddAsync(contract);

            return Ok(cntrct);

        }

        [HttpPut, Route("UpdateContracts/{contractId}")]
        public IActionResult ContractsUpdate(int contractId, [FromBody] Contracts contract)
        {
            // Eğer ID'ler uyuşmazsa hata dönebiliriz
            if (contractId != contract.ContractId)
            {
                return BadRequest("Contract ID uyusmadi.");
            }
            //usrDto.Id = Guid.NewGuid();
            var bus = _contractsService.Update(contract);
            if (bus == null)
            {
                return NotFound("Contract not found.");
            }

            return Ok();

        }

        [HttpDelete, Route("DeleteContract/{Contract}")]
        public IActionResult ContractDelete(Contracts contract)
        {
            //usrDto.Id = Guid.NewGuid();


            _contractsService.Remove(contract);
            return Ok();
        }

    }
}
