using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        bool CreateReview(Review review);
        bool Save();
    }
}
