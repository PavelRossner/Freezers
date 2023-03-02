using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Mrazaky.Models;
using System.Reflection.Emit;
using NuGet.DependencyResolver;
using System.Diagnostics.Metrics;
using Mrazaky.Config;

namespace Mrazaky.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationUserFreezer> ApplicationUserFreezer { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodCategory> FoodCategory { get; set; }
        public DbSet<Freezer> Freezer { get; set; }
        public DbSet<FreezerFood> FreezerFood { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserFreezerConfiguration());
            modelBuilder.ApplyConfiguration(new DashboardViewConfiguration());
            modelBuilder.ApplyConfiguration(new FoodConfiguration());
            modelBuilder.ApplyConfiguration(new FoodCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FreezerConfiguration());
            modelBuilder.ApplyConfiguration(new FreezerFoodConfiguration());
        }
    }
}