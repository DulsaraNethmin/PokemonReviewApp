using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly ReviewerRepository _reviewerRepository;

        public ReviewerController(ReviewerRepository reviewerRepository)
        {
            this._reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _reviewerRepository.GetReviewers();

            if (ModelState.IsValid)
            {
                return Ok(reviewers);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewer(int id)
        {
            var reviewer = _reviewerRepository.GetReviewer(id);

            if (ModelState.IsValid)
            {
                return Ok(reviewer);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
