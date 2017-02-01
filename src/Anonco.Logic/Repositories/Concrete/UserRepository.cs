namespace Anonco.Logic.Repositories.Concrete
{
    using System.Data.Entity;
    using System.Linq;
    using Abstract;
    using Database;
    using Database.Entities;

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            var users = _context.Users.AsNoTracking().OrderBy(a => a.LastName);
            return users;
        }

        public User GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
