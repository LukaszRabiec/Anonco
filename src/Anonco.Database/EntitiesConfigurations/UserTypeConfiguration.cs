namespace Anonco.Database.EntitiesConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    public class UserTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserTypeConfiguration()
        {
            Property(u => u.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            Property(u => u.LastName)
                .HasMaxLength(64)
                .IsRequired();
        }
    }
}
