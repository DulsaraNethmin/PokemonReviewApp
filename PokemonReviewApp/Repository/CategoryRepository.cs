using PokemonReviewApp.data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context )
        {
            this._context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any<Category>(c => c.Id == id); 
        }

        public bool createCategory(Category category)
        {
           _context.Add(category);
            return save();
        }

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(pc => pc.CategoryId == categoryId).Select(p => p.Pokemon).ToList();
        }

        public bool save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
