using System.Linq;

namespace Anonco.Logic.Repositories.Abstract
{
    using Database.Entities;

    public interface IAnnouncementRepository
    {
        IQueryable<Announcement> GetAll();
        Announcement GetById(int id);
        void Add(Announcement announcement);
        void Delete(int id);
        void SaveChanges();
    }
}
