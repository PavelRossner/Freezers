using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasKey(p => p.FoodId);

            builder.Property(p => p.FoodId).HasComment("FoodId");
            builder.Property(p => p.Category).HasComment("Category");
            builder.Property(p => p.FoodName).HasComment("FoodName");
            builder.Property(p => p.FreezerName).HasComment("FreezerName");
            builder.Property(p => p.NumberOfPackages).HasComment("NumberOfPackages");
            builder.Property(p => p.Weight).HasComment("Weight");
            builder.Property(p => p.PackageLabel).HasComment("PackageLabel");
            builder.Property(p => p.FreezerPosition).HasComment("FreezerPosition");
            builder.Property(p => p.DatePurchase).HasComment("DatePurchase");
            builder.Property(p => p.BestBefore).HasComment("BestBefore");
            builder.Property(p => p.Note).HasComment("Note");

            OnConfigurePartial(builder);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<Food> entity);
    }
}