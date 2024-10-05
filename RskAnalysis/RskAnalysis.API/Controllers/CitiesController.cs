using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using RskAnalysis.CORE.IntServices.IntCitiesServ;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA;

namespace RskAnalysis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CitiesController(ICitiesService citiesService, AppDbContext context, IMapper mapper)
        {
            _citiesService= citiesService;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet, Route("CitiesList")]
        public async Task<IActionResult> GetCitiesList()
        {
            var cityList = await _citiesService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CitiesDto>>(cityList));
        }

        [HttpGet, Route("Cities/{id}")]
        public async Task<IActionResult> GetCitiesById(int id)
        {
            var cty = await _citiesService.GetByIdAsync(id);

            //return Ok(_mapper.Map<IEnumerable<CitiesDto>>(cty));
            return Ok(cty);


        }

        [HttpPost, Route("AddCity/{City}")]
        public IActionResult CityAdd(Cities city)
        {
            
            //usrDto.Id = Guid.NewGuid();
            var cty = _citiesService.AddAsync(city);
            
            

            return Ok(cty);
            //return CreatedAtAction(nameof(GetCitiesList), city);

        }

        [HttpPut, Route("UpdateCity/{City}")]
        public IActionResult CityUpdate(Cities city)
        {
            //usrDto.Id = Guid.NewGuid();
            var cty = _citiesService.Update(city);

            return Ok(cty);

        }

        [HttpDelete, Route("DeleteCity/{City}")]
        public IActionResult CityDelete(Cities city)
        {
            //usrDto.Id = Guid.NewGuid();


            _citiesService.Remove(city);
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
