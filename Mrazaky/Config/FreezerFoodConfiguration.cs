using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class FreezerFoodConfiguration : IEntityTypeConfiguration<FreezerFood>
    {
        public void Configure(EntityTypeBuilder<FreezerFood> builder)
        {
            builder.HasKey(p => p.FreezerFoodID);

            builder.Property(p => p.FreezerFoodID).HasComment("FreezerFoodID");
            builder.Property(p => p.FoodID).HasComment("FoodID");
            builder.Property(p => p.FreezerID).HasComment("FreezerID");
            builder.Property(p => p.FreezerName).HasComment("FreezerLocation");
            builder.Property(p => p.Category).HasComment("Category");

            builder.HasOne(d => d.Freezer)
                .WithMany(od => od.FreezerFood)
                .HasForeignKey(d => d.FreezerID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FreezerFood_Freezer");

            builder.HasOne(d => d.Food)
                .WithMany(od => od.FreezerFood)
                .HasForeignKey(d => d.FoodID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FreezerFood_Food");

            OnConfigurePartial(builder);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<FreezerFood> entity);
    }
}