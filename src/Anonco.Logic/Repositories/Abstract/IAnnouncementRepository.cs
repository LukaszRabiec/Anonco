using System.Linq;

namespace Anonco.Logic.Repositories.Abstract
{
    using Database.Entities;

    public interface IAnnouncementRepository
    {
        IQueryable<Announcement> GetAll();
        IQueryable<Announcement> GetPage(int? page, int? pageSize);
        Announcement GetById(int id);
        void Add(Announcement announcement);
        void Update(Announcement announcement);
        void Delete(int id);
        void SaveChanges();
    }
}
