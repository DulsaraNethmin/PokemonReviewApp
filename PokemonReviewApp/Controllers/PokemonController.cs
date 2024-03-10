using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private const int SUCESS = 200;
        private readonly IPokemonRepository _repository;
        private readonly IMapper _mapper;

        public PokemonController( IPokemonRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(SUCESS, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<PokemonDto>(_repository.GetPokemons());
            
            if(ModelState.IsValid)
            {
                return Ok(pokemons);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(SUCESS, Type = typeof(Pokemon))]
        public IActionResult GetPokemon(int id)
        {
            bool pokemonExist = _repository.PokemonExists(id);
            if (!pokemonExist)
            {
                return NotFound();
            }
            var pokemon = _mapper.Map<PokemonDto>(_repository.GetPokemon(id));
            
            if(ModelState.IsValid)
            {
                return Ok(pokemon);
            }
            else
            {
                return BadRequest(ModelState);
            }   
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(SUCESS, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByName (string name)
        {
            var pokemon = _repository.GetPokemon(name);
            
            if(ModelState.IsValid)
            {
                return Ok(pokemon);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("rating/{id}")]
        [ProducesResponseType(SUCESS, Type = typeof(decimal))]
        public IActionResult GetRating(int id)
        {
            bool pokemon = (_repository.PokemonExists(id));
            if(!pokemon)
            {
                return NotFound();
            }
            var rating = _repository.GetRating(id);
            
            if(ModelState.IsValid)
            {
                return Ok(rating);
            }
            else
            {
                return BadRequest(ModelState);
            }   
        }

        [HttpGet("exists/{id}")]
        [ProducesResponseType(SUCESS, Type = typeof(bool))]
        public IActionResult PokemonExists(int id)
        {
            var exists = _repository.PokemonExists(id);
            
            if(ModelState.IsValid)
            {
                return Ok(exists);
            }
            else
            {
                return BadRequest(ModelState);
            }   
        }

    }
}
