using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesRepository _citiesRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CitiesController(ICitiesRepository citiesRepository, AppDbContext context, IMapper mapper)
        {
            _citiesRepository = citiesRepository;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet, Route("CitiesList")]
        public async Task<IActionResult> GetCitiesList()
        {
            var cityList = await _citiesRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CitiesDto>>(cityList));
        }

        [HttpGet, Route("Cities/{id}")]
        public async Task<IActionResult> GetCitiesById(int id)
        {
            var cty = await _citiesRepository.GetByIdAsync(id);

            //return Ok(_mapper.Map<IEnumerable<CitiesDto>>(cty));
            return Ok(cty);

        }

        [HttpPost, Route("AddCity/{City}")]
        public IActionResult CityAdd(Cities city)
        {
            //usrDto.Id = Guid.NewGuid();
            var cty = _citiesRepository.AddAsync(city);

            //return Ok(cty);

            return CreatedAtAction(nameof(GetCitiesList), new { id = city.CityId }, city);

        }

        [HttpPut, Route("UpdateCity/{City}")]
        public IActionResult CityUpdate(Cities city)
        {
            //usrDto.Id = Guid.NewGuid();
            var cty = _citiesRepository.Update(city);

            return Ok(cty);

        }

        [HttpDelete, Route("DeleteCity/{City}")]
        public IActionResult CityDelete(Cities city)
        {
            //usrDto.Id = Guid.NewGuid();


            _citiesRepository.Remove(city);
            return Ok();

           


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
