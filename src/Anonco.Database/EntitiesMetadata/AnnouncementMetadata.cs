namespace Anonco.Database.EntitiesMetadata
{
    using System.ComponentModel.DataAnnotations;

    public class AnnouncementMetadata
    {
        [Required(ErrorMessage = "You must specify title.")]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "The {0} length must be between {2}-{1} characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must specify content.")]
        [StringLength(1024, MinimumLength = 16, ErrorMessage = "The {0} length must be between {2}-{1} characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "You must specify email.")]
        [EmailAddress(ErrorMessage = "Invalid e-mail.")]
        public string Email { get; set; }

        [RegularExpression("\\+[0-9]{2}[ ][0-9]{3}[ ][0-9]{3}[ ][0-9]{3}",
            ErrorMessage = "Correct phone number: +XX XXX XXX XXX.")]
        public string PhoneNumber { get; set; }
    }
}
