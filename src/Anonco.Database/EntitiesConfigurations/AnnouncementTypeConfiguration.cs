namespace Anonco.Database.EntitiesConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;
    public class AnnouncementTypeConfiguration : EntityTypeConfiguration<Announcement>
    {
        public AnnouncementTypeConfiguration()
        {
            HasRequired(a => a.User)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(true);

            Property(a => a.Title)
                .HasMaxLength(64)
                .IsRequired();

            Property(a => a.Content)
                .HasMaxLength(1024)     // I like round numbers :D
                .IsRequired();

            Property(a => a.Email)
                .HasMaxLength(64)
                .IsRequired();

            Property(a => a.PhoneNumber)
                .HasMaxLength(15);
        }
    }
}
