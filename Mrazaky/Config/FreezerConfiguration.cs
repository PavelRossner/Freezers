using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class FreezerConfiguration : IEntityTypeConfiguration<Freezer>
    {
        public void Configure(EntityTypeBuilder<Freezer> builder)
        {
            builder.HasKey(p => p.FreezerId);

            builder.Property(p => p.FreezerId).HasComment("FreezerId");
            builder.Property(p => p.FreezerName).HasComment("FreezerName");
            builder.Property(p => p.NumberOfShelves).HasComment("NumberOfShelves");
            builder.Property(p => p.LastDefrosted).HasComment("LastDefrosted");
            builder.Property(p => p.DefrostInterval).HasComment("DefrostInterval");
            builder.Property(p => p.Note).HasComment("Note");

            builder.HasIndex(fl => fl.FreezerName)  //This links a concrete freezer (FreezerId) with its name (FreezerName)
                   .IsUnique();

            OnConfigurePartial(builder);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<Freezer> entity);
    }
}