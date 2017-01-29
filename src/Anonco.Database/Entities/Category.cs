namespace Anonco.Database.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EntitiesMetadata;

    [MetadataType(typeof(CategoryMetadata))]
    public class Category
    {
        public Category()
        {
            AnnouncementCategory = new HashSet<AnnouncementCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }


        public ICollection<AnnouncementCategory> AnnouncementCategory { get; set; }
    }
}
