using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _repository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_repository.GetCountries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(countries);
        }

        [HttpGet("{id}")]
          [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountry(int id)
        {
            if (!_repository.CountryExists(id))
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDto>(_repository.GetCountry(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryByName(string name)
        {
            var country = _mapper.Map<CountryDto>(_repository.GetCountryByName(name));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpGet("{countryId}/owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwnersByCountryId(int countryId)
        {
            var owners = _mapper.Map<List<OwnerDto>>(_repository.GetOwnersByCountryId(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }
    }
}
