using System.Linq;

namespace Anonco.Logic.Repositories.Abstract
{
    using Database.Entities;

    public interface ICategoryRepository
    {
        IQueryable<Category> GetAll();
    }
}
