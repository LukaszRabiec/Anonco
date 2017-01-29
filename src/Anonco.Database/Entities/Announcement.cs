using System;

namespace Anonco.Database.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EntitiesMetadata;

    [MetadataType(typeof(AnnouncementMetadata))]
    public class Announcement
    {
        public Announcement()
        {
            AnnouncementCategory = new HashSet<AnnouncementCategory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset AdditionDate { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<AnnouncementCategory> AnnouncementCategory { get; set; }
    }
}
