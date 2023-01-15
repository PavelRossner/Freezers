using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Mrazaky.Models;
using System.Reflection.Emit;
using NuGet.DependencyResolver;
using System.Diagnostics.Metrics;

namespace Mrazaky.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Food> Food { get; set; }
        public DbSet<Freezer> Freezer { get; set; }
        public DbSet<ApplicationUser> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Food>()
            .HasOne(u => u.User)
            .WithMany(fo => fo.Foods)
            .HasForeignKey(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Freezer>()
            .HasOne(u => u.User)
            .WithMany(fr => fr.Freezers)
            .HasForeignKey(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Freezer>()
            .HasMany(t => t.Foods)
            .WithMany(t => t.Freezers);

            //builder
            //.Entity<FoodFreezer>()
            //.HasKey(ff => ff.FoodFreezerId);

            //builder.Entity<FoodFreezer>()
            //    .HasKey(e => new { e.FoodId, e.FreezerId });

            //builder
            //    .Entity<FoodFreezer>()
            //    .HasOne(fo => fo.Food)
            //    .WithMany(ff => ff.FoodFreezers)
            //    .HasForeignKey(fi => fi.FoodId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<FoodFreezer>()
            //    .HasOne(fr => fr.Freezer)
            //    .WithMany(ff => ff.FoodFreezers)
            //    .HasForeignKey(fri => fri.FreezerId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}