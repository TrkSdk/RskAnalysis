using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.CORE.IntServices.IntRejectedContractsServ;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectedContractsController : ControllerBase
    {
        private readonly IRejectedContractsService _rejectedcontractsService;
        private readonly IMapper _mapper;

        public RejectedContractsController(IRejectedContractsService rejectedcontractsService, AppDbContext context, IMapper mapper)
        {
            _rejectedcontractsService = rejectedcontractsService;
            _mapper = mapper;
        }

        [HttpGet, Route("RejectedContractsList")]
        public async Task<IActionResult> GetRejectedContractsList()
        {
            var contrList = await _rejectedcontractsService.GetAllAsync();
            contrList = contrList.Where(x => x.IsRejected == true);
            return Ok(_mapper.Map<IEnumerable<ContractsDto>>(contrList));
        }

        [HttpGet, Route("RejectedContractsWithPartner")]
        public async Task<IActionResult> GetRejectedContractWithPartner()
        {
            var parList = await _rejectedcontractsService.GetRejectedContractWithPartners();
            return Ok(parList);
        }


        [HttpGet, Route("RejectedContractsID/{id}")]
        public async Task<IActionResult> GetRejectedContractById(int id)
        {
            var cntrct = await _rejectedcontractsService.GetByIdAsync(id);

            return Ok(cntrct);

        }

        [HttpGet, Route("RejectedContractsIDWithPartner/{id}")]
        public async Task<IActionResult> GetRejectedContractByIdWithPartner(int id)
        {
            var cntrct = await _rejectedcontractsService.GetRejectedContractByIdWithPartners(id);

            return Ok(cntrct);

        }

        [HttpPost, Route("AddRejectedContracts/{RejectedContract}")]
        public IActionResult ContractAdd(Contracts contract)
        {
            //contract.Partner = null;
            contract.IsRejected= true;
            var cntrct = _rejectedcontractsService.AddAsync(contract);

            return Ok(cntrct);

        }

        [HttpPut, Route("UpdateRejectedContract/{RejectedContract}")]
        public IActionResult RejectedContractsUpdate(int ContractId, [FromBody] Contracts rejectedcontract)
        {

            var bus = _rejectedcontractsService.Update(rejectedcontract);
            if (bus == null)
            {
                return NotFound("Contract not found.");
            }

            return Ok();

        }

        [HttpDelete, Route("DeleteRejectedContract/{RejectedContract}")]
        public IActionResult RejectedContractDelete(Contracts contract)
        {
            //usrDto.Id = Guid.NewGuid();


            _rejectedcontractsService.Remove(contract);
            return Ok();
        }

    }
}
