using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.CORE.IntServices.IntRejectedContractsServ;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;
using RskAnalysis.DATA.Repository.CitiesRepo;
using RskAnalysis.DATA.Repository.ContractsRepo;
using RskAnalysis.DATA.Repository.RejectedContractsRepo;
//using System.Diagnostics.RejectedContracts;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectedContractsController : ControllerBase
    {
        private readonly IRejectedContractsService _rejectedContractsService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RejectedContractsController(IRejectedContractsService rejectedContractsService, AppDbContext context, IMapper mapper)
        {
            _rejectedContractsService= rejectedContractsService;
            _context = context;
            _mapper = mapper;
        }

        // GET: RejectedContracts
        [HttpGet, Route("RejectedContractsList")]
        public async Task<IActionResult> GetContractsList()
        {
            var rejectedcontrList = await _rejectedContractsService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RejectedContractsDto>>(rejectedcontrList));
        }

        // GET: RejectedContracts/Details/5
        [HttpGet, Route("RejectedContracts/{id}")]
        public async Task<IActionResult> GetRejectedContractsById(int id)
        {
            var contr = await _rejectedContractsService.GetByIdAsync(id);

            return Ok(_mapper.Map<IEnumerable<RejectedContractsDto>>(contr));

        }

        // GET: RejectedContracts/Create
        [HttpPost, Route("AddRejectedContract/{RejectedContract}")]
        public IActionResult RejectedContractAdd(RejectedContracts rejectedcontract)
        {
            //usrDto.Id = Guid.NewGuid();
            var rejectedcontr = _rejectedContractsService.AddAsync(rejectedcontract);

            return Ok(_mapper.Map<IEnumerable<RejectedContractsDto>>(rejectedcontr));

        }

        // POST: RejectedContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("RejectedContractId,Amount,BusinessId,PartnerId,ContractName,StartDate,EndDate,CreatedDate")] RejectedContracts rejectedContracts)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(rejectedContracts);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", rejectedContracts.BusinessId);
        //    ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", rejectedContracts.PartnerId);
        //    return View(rejectedContracts);
        //}

        // GET: RejectedContracts/Edit/5
        [HttpPut, Route("UpdateRejectedContract/{RejectedContract}")]
        public IActionResult RejectedContractUpdate(RejectedContracts rejectedcontract)
        {
            //usrDto.Id = Guid.NewGuid();
            var contr = _rejectedContractsService.Update(rejectedcontract);

            return NoContent();

        }

        // POST: RejectedContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("RejectedContractId,Amount,BusinessId,PartnerId,ContractName,StartDate,EndDate,CreatedDate")] RejectedContracts rejectedContracts)
        //{
        //    if (id != rejectedContracts.RejectedContractId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(rejectedContracts);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RejectedContractsExists(rejectedContracts.RejectedContractId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", rejectedContracts.BusinessId);
        //    ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", rejectedContracts.PartnerId);
        //    return View(rejectedContracts);
        //}

        // GET: RejectedContracts/Delete/5
        [HttpDelete, Route("DeleteRejectedContract/{RejectedContract}")]
        public IActionResult RejectedContractDelete(RejectedContracts rejectedcontract)
        {
            //usrDto.Id = Guid.NewGuid();


            _rejectedContractsService.Remove(rejectedcontract);
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
        // POST: RejectedContracts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.RejectedContracts == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.RejectedContracts'  is null.");
        //    }
        //    var rejectedContracts = await _context.RejectedContracts.FindAsync(id);
        //    if (rejectedContracts != null)
        //    {
        //        _context.RejectedContracts.Remove(rejectedContracts);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RejectedContractsExists(int id)
        //{
        //  return (_rejectedContractsRepository.Any(e => e.RejectedContractId == id)).GetValueOrDefault();
        //}
    }
}
