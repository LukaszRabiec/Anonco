namespace Anonco.Database.Entities
{
    public class AnnouncementCategory
    {
        public int Id { get; set; }
        public int AnnouncementId { get; set; }
        public int CategoryId { get; set; }


        public virtual Announcement Announcement { get; set; }
        public virtual Category Category { get; set; }
    }
}
