using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName).HasComment("User's first name");
            builder.Property(p => p.LastName).HasComment("User's last name");

            OnConfigurePartial(builder);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ApplicationUser> entity);
    }
}