using System.Linq;

namespace Anonco.Logic.Repositories.Concrete
{
    using Abstract;
    using Database;
    using Database.Entities;

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            var categories = _context.Categories.AsNoTracking();

            return categories;
        }
    }
}
