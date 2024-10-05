using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntServices.IntSectorsServ;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly ISectorsService _sectorsService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SectorsController(ISectorsService sectorsService, AppDbContext context, IMapper mapper)
        {
            _sectorsService = sectorsService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("SectorsList")]
        public async Task<IActionResult> GetSectorsList()
        {
            var secList = await _sectorsService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SectorsDto>>(secList));
        }

        [HttpGet, Route("Sectors/{id}")]
        public async Task<IActionResult> GetSectorsById(int id)
        {
            var sec = await _sectorsService.GetByIdAsync(id);

            return Ok(sec);

        }

        [HttpPost, Route("AddSector/{Sector}")]
        public IActionResult SectorsAdd(Sectors sector)
        {
            //usrDto.Id = Guid.NewGuid();
            var sec = _sectorsService.AddAsync(sector);

            //return Ok(_mapper.Map<IEnumerable<SectorsDto>>(sec));
            //return CreatedAtAction(nameof(GetSectorsList), new { id = sector.SectorId }, sector);
            return Ok(sec);
        }

        [HttpPut, Route("UpdateSector/{Sector}")]
        public IActionResult SectorsUpdate(Sectors sector)
        {
            //usrDto.Id = Guid.NewGuid();
            var sec = _sectorsService.Update(sector);

            return Ok(sec);

        }

        [HttpDelete, Route("DeleteSectors/{Sector}")]
        public IActionResult SectorsDelete(Sectors sector)
        {
            //usrDto.Id = Guid.NewGuid();


            _sectorsService.Remove(sector);
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
