namespace Anonco.Database.EntitiesMetadata
{
    using System.ComponentModel.DataAnnotations;

    public class AnnouncementMetadata
    {
        [Required(ErrorMessage = "You must specify title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must specify content.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "You must specify email.")]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("\\+[0-9]{2}[ ][0-9]{3}[ ][0-9]{3}[ ][0-9]{3}",
            ErrorMessage = "Correct phone number: '+XX XXX XXX XXX'.")]
        public string PhoneNumber { get; set; }
    }
}
