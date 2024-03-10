using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper )
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_repository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(int id) 
        {
            if(!_repository.CategoryExists(id))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_repository.GetCategory(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_repository.GetPokemonByCategory(categoryId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryToCreate)
        {
            if(categoryToCreate == null)
            {
                return BadRequest(ModelState);
            }

            //var category = _mapper.Map<CategoryDto>(categoryToCreate);

            var category = _repository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryToCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (category != null)
            { 
                ModelState.AddModelError("", "Category already Exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finalCategory = _mapper.Map<Category>(categoryToCreate);

            if (!_repository.createCategory(finalCategory))
            {
                ModelState.AddModelError("", "Something Went Wrong.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created.");
        }

    }
}
