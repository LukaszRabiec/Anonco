using System.Linq;

namespace Anonco.Logic.Repositories.Concrete
{
    using System.Data.Entity;
    using Abstract;
    using Database;
    using Database.Entities;

    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Announcement> GetAll()
        {
            var announcements = _context.Announcements.AsNoTracking().OrderByDescending(a => a.AdditionDate);
            return announcements;
        }

        public IQueryable<Announcement> GetPage(int? page = 1, int? pageSize = 10)
        {
            var announcements = _context.Announcements
                .OrderByDescending(a => a.AdditionDate)
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

            return announcements;
        }

        public Announcement GetById(int id)
        {
            return _context.Announcements.Find(id);
        }

        public void Add(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
        }

        public void Update(Announcement announcement)
        {
            _context.Entry(announcement).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var announcement = _context.Announcements.Find(id);
            _context.Announcements.Remove(announcement);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
