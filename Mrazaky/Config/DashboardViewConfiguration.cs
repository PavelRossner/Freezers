using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mrazaky.Models;

namespace Mrazaky.Config
{
    public partial class DashboardViewConfiguration : IEntityTypeConfiguration<DashboardViewModel>
    {
        public void Configure(EntityTypeBuilder<DashboardViewModel> builder)
        {
            builder.HasKey(p => p.DashboardViewModelId);

            builder.Property(p => p.DashboardViewModelId).HasComment("DashboardViewModelId");
            builder.Property(p => p.FoodCount).HasComment("FoodCount");
            builder.Property(p => p.FreezerCount).HasComment("FreezerCount");
            builder.Property(p => p.FoodCategoryCount).HasComment("FoodCategoryCount");
            builder.Property(p => p.FreezerFoodCount).HasComment("FreezerFoodCount");
            builder.Property(p => p.ExpiredFood).HasComment("ExpiredFood");
            builder.Property(p => p.NonExpiredFood).HasComment("NonExpiredFood");

            OnConfigurePartial(builder);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<DashboardViewModel> entity);
    }
}