using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System;
using Microsoft.AspNetCore.Identity;

namespace SkateAppWeb.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skateboard>()
                .HasOne(p => p.Accessory)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Skateboard>()
               .HasOne(p => p.Bearing)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Skateboard>()
               .HasOne(p => p.Deck)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Skateboard>()
               .HasOne(p => p.Griptape)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Skateboard>()
               .HasOne(p => p.Truck)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Skateboard>()
               .HasOne(p => p.Wheel)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);




            modelBuilder.Entity<CompleteSkateboard>()
              .HasOne(p => p.Bearing)
              .WithMany()
              .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<CompleteSkateboard>()
               .HasOne(p => p.Griptape)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<CompleteSkateboard>()
               .HasOne(p => p.Truck)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<CompleteSkateboard>()
               .HasOne(p => p.Wheel)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientNoAction);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bearing> Bearings { get; set; }
        public DbSet<CompleteSkateboard> CompleteSkateboards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Griptape> Griptapes { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Wheel> Wheels { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Skateboard> Skateboards { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
		public DbSet<Review> Reviews { get; set; }
        public DbSet<Blog> Blogs { get; set; }
		public DbSet<Product> Products { get; set; }
    }
}