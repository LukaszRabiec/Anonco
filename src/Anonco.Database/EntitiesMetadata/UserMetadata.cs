namespace Anonco.Database.EntitiesMetadata
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserMetadata
    {
        [Required(ErrorMessage = "You must specify your first name.")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "The {0} length must be between {2}-{1} characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must specify your last name.")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "The {0} length must be between {2}-{1} characters.")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get; set; }
    }
}
