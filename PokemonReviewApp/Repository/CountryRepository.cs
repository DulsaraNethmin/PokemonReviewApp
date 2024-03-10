using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Find(id);
        }

        public ICollection<Owner> GetOwnersByCountryId(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public Country GetCountryByName(string name)
        {
            return _context.Countries.Where(c => c.Name == name).FirstOrDefault();
        }

        public bool CreateCountry(Country country)
        {
           _context.Add(country);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
