using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        public Country GetCountry(int id);
        public Country GetCountryByName(string name);
        public ICollection<Owner> GetOwnersByCountryId(int countryId);
        public bool CountryExists(int id);
    }
}
