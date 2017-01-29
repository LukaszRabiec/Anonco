namespace Anonco.Database.EntitiesMetadata
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryMetadata
    {
        [Required(ErrorMessage = "You must specify category name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must specify parent category.")]
        public int ParentId { get; set; }
    }
}
