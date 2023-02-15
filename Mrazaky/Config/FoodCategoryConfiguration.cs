using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class FoodCategoryConfiguration : IEntityTypeConfiguration<FoodCategory>
    {
        public void Configure(EntityTypeBuilder<FoodCategory> builder)
        {
            builder.HasKey(p => p.FoodCategoryId);

            builder.Property(p => p.FoodCategoryId).HasComment("FoodCategoryId");
            builder.Property(p => p.FoodCategoryName).HasComment("Category");


            OnConfigurePartial(builder);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<FoodCategory> entity);
    }
}
