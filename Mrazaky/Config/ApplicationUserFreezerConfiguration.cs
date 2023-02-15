using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class ApplicationUserFreezerConfiguration : IEntityTypeConfiguration<ApplicationUserFreezer>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserFreezer> builder)
        {
            builder.HasComment("Holds information about user's freezers");

            builder.HasKey(p => p.ApplicationUserFreezerID);

            builder.Property(p => p.ApplicationUserFreezerID).HasComment("Gets or sets the user freezer identifier.");
            builder.Property(p => p.ApplicationUserID).HasComment("Gets or sets the user identifier.");
            builder.Property(p => p.FreezerID).HasComment("Gets or sets the freezed identifier.");
            builder.Property(p => p.CreateAt).HasComment("Gets or sets the create at.");
            builder.Property(p => p.IsActive).HasComment(" Gets or sets a value indicating whether this instance is active.");

            // Dependency one to many
            // "Many" from UserFreezer to "one" on Freezer
            builder.HasOne(d => d.Freezer) // d -> dependency
                   .WithMany(od => od.UserFreezer)// od -> other dependency
                   .HasForeignKey(d => d.FreezerID)  //originally .HasForeignKey(d => d.ApplicationUserFreezerID)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_UserFreezer_Freezer");


            // Dependency one to many
            // "Many" from UserFreezer to "one" on ApplicationUser
            builder.HasOne(d => d.ApplicationUser)
                   .WithMany(od => od.UserFreezer)
                   .HasForeignKey(d => d.ApplicationUserID)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_UserFreezer_ApplicationUser");

            // TODO
            // In other words:
            // The basic idea is,
            // -> User can have multiple freezers.
            // -> Freezer can be owned by multiple users
            // To keep track which freezer belongs to which users, we create a "junction" table,
            // which will hold information about relationships between freezers and users.
            // If we do not resolve many-to-many relationship using the junction table,
            // we will be forced to have multiple records in one column for a specific row => not acceptable.
            // The rule is => one column, one value

            OnConfigurePartial(builder);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<ApplicationUserFreezer> entity);
    }
}