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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }


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

            builder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(p => p.Cart)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasMany(c => c.CartItems)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CartItem>()
                 .HasOne(c => c.Cart)
                 .WithMany(p => p.CartItems)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CartItem>()
                 .HasOne(c => c.Product)
                 .WithMany(p => p.CartItems)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Favorite>()
               .HasMany(c => c.FavoriteItems)
               .WithOne(p => p.Favorite)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasMany(c => c.FavoriteItems)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<FavoriteItem>()
                 .HasOne(c => c.Favorite)
                 .WithMany(p => p.FavoriteItems)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<FavoriteItem>()
                 .HasOne(c => c.Product)
                 .WithMany(p => p.FavoriteItems)
            .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(builder);
        }
    }
}
