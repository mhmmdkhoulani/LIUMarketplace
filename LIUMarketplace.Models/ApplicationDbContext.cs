using LIUMarketplace.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Products)
                .WithOne(u => u.CreatedByUser)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Reviews)
                .WithOne(u => u.CreatedByUser)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Media)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Product>()
                .HasMany(c => c.Reviews)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasMany(c => c.MediaPaths)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}
