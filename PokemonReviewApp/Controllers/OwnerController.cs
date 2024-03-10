using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository , IMapper mapper)
        {
            this._ownerRepository = ownerRepository;
            this._countryRepository = countryRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (ModelState.IsValid)
            {
                return Ok(owners);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        public IActionResult GetOwner(int id)
        {
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));

            if (ModelState.IsValid)
            {
                return Ok(owner);
            }
            else
            {
                return BadRequest(ModelState);
            }   
        }

        [HttpGet("name/{firstName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwnersByFirstName (string firstName)
        {
            var owners = _ownerRepository.GetOwnersByFirstName(firstName);

            if (ModelState.IsValid)
            {
                return Ok(owners);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateOwner( [FromBody] OwnerCreateDto ownerToCreate)
        {
            if (ownerToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var owner = _mapper.Map<Owner>(ownerToCreate);

            owner.Country = _countryRepository.GetCountry(ownerToCreate.CountryId);

            if (!_ownerRepository.CreateOwner(owner))
            {
                ModelState.AddModelError("", $"Something went wrong saving the owner {owner.FirstName} {owner.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok("SucessFully Created.");
        }   

    }
}
