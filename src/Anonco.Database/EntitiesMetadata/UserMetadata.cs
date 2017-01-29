namespace Anonco.Database.EntitiesMetadata
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserMetadata
    {
        [Required(ErrorMessage = "You must specify your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must specify your last name.")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get; set; }
    }
}
