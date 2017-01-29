namespace Anonco.Database.EntitiesConfigurations
{
    using System.Data.Entity.ModelConfiguration;
    using Entities;

    public class CategoryTypeConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryTypeConfiguration()
        {
            Property(c => c.Name)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
