using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository , IMapper mapper)
        {
            this._reviewRepository = reviewRepository;
            this._pokemonRepository = pokemonRepository;
            this._reviewerRepository = reviewerRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Review))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateReview([FromBody] ReviewCreateDto reviewCreateDto) 
        {
            if(reviewCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            var reviewer = _reviewerRepository.GetReviewer(reviewCreateDto.ReviewerId);
            if(reviewer == null)
            {
                ModelState.AddModelError("", "Reviewer not found.");
                return StatusCode(422, ModelState);
            }

            var pokemon = _pokemonRepository.GetPokemon(reviewCreateDto.PokemonId);
            if(pokemon == null)
            {
                ModelState.AddModelError("", "Pokemon not found.");
                return StatusCode(422, ModelState);
            }

            var review = _mapper.Map<Review>(reviewCreateDto);
            review.Reviewer = reviewer;
            review.Pokemon = pokemon;

            if(!_reviewRepository.CreateReview(review))
            {
                ModelState.AddModelError("", $"Something went wrong saving the review {review.Id}");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created.");

        }

    }
}
