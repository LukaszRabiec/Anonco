using System.Threading.Tasks;

namespace Anonco.Database.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using EntitiesMetadata;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    [MetadataType(typeof(UserMetadata))]
    public class User : IdentityUser
    {
        public User()
        {
            Announcements = new HashSet<Announcement>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        #region NotMapped

        public string FullName => $"{FirstName} {LastName}";

        #endregion


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
