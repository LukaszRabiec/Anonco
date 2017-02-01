using System.Linq;

namespace Anonco.Logic.Repositories.Abstract
{
    using Database.Entities;

    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User GetById(string id);
        void Delete(User user);
        void SaveChanges();
    }
}
