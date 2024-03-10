using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context )
        {
            this._context = context;
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Find(id);
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Owner> GetOwnersByFirstName(string firstName)
        {
            return _context.Owners.Where(o => o.FirstName == firstName ).ToList();
        }
    }
}
