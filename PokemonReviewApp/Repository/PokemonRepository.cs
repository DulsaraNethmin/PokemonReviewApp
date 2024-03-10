using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Find(id);
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.ToList();  
        }

        public decimal GetRating(int id)
        {
            var review = _context.Reviews.Where(r => r.Pokemon.Id == id ).ToList();

            if(review.Count == 0)
            {
                return 0;
            }
            double rating  = review.Average(r => r.Rating);
            return (decimal)rating;
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }
    }
}
