using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetRating(int id);
        bool PokemonExists(int id);
        bool CreatePokemon(Pokemon pokemon, int ownerId, int categoryId);
        bool save();
    }
}
