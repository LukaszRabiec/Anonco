namespace Anonco.Database.EntitiesMetadata
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryMetadata
    {
        [Required(ErrorMessage = "You must specify category name.")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "The {0} length must be between {2}-{1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must specify parent category.")]
        public int ParentId { get; set; }
    }
}
