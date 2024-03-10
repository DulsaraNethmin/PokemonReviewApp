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

        public bool CreatePokemon(Pokemon pokemon, int ownerId, int categoryId)
        {
            var owner = _context.Owners.Find(ownerId);
            var category = _context.Categories.Find(categoryId);

            PokemonOwner pokemonOwner = new PokemonOwner();
            pokemonOwner.Owner = owner;
            pokemonOwner.Pokemon = pokemon;
            _context.Add(pokemonOwner);

            PokemonCategory pokemonCategory = new PokemonCategory();
            pokemonCategory.Category = category;
            pokemonCategory.Pokemon = pokemon;
            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return save();
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

        public bool save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
