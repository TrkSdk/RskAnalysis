using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using RskAnalysis.DATA;
using System;
using RskAnalysis.API.DTOs;
using AutoMapper;
using System.Security.Cryptography;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessesService _businessService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BusinessesController(IBusinessesService businessService,AppDbContext context, IMapper mapper)
        {
            _businessService=businessService;
            _context=context;
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
            var busId = await _businessService.GetByIdAsync(id);

            return Ok(busId);

        }

        [HttpGet, Route("BusinessesIDWithSector/{id}")]
        public async Task<IActionResult> GetBusinessesByIdWithSector(int id)
        {
            var busId = await _businessService.GetBussinessByIdWithSectors(id);

            return Ok(busId);

        }

        [HttpPost, Route("AddBusinesses/{business}")]
        public IActionResult BusinessesAdd(Businesses business)
        {
            //usrDto.Id = Guid.NewGuid();
            var bus = _businessService.AddAsync(business);

            //return Ok(_mapper.Map<IEnumerable<BusinessesDto>>(bus));
            return CreatedAtAction(nameof(GetBussinessesList), new { id = business.BusinessId }, business);
        }

        [HttpPut, Route("UpdateBusinesses/{business}")]
        public IActionResult BusinessesUpdate(Businesses business)
        {
            //usrDto.Id = Guid.NewGuid();
            var bus = _businessService.Update(business);

            return NoContent();

        }

        [HttpDelete, Route("DeleteBusinesses/{business}")]
        public IActionResult BusinessesDelete(Businesses business)
        {
            //usrDto.Id = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                _businessService.Remove(business);
                return NoContent();
            }
            
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
