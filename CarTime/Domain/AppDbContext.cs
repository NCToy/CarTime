using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarTime.Domain.Entities;
using CarTime.Models;

namespace CarTime.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CarItem> CarItems { get; set; }
        public DbSet<BrandItem> BrandItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<ShopCartItem> ShopCartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ReturnOrder> ReturnOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "91f88ad6-fbdf-432e-a5cb-828e97def61d",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "e3ee9b61-b13d-409f-bcd6-cc0210b82cbd",
                Name = "manager",
                NormalizedName = "MANAGER"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "26CB2F53-B690-4854-84F1-28DF22C44C28",
                Name = "user",
                NormalizedName = "USER"
            });

            modelBuilder.Entity<UserData>().HasData(new UserData
            {
                Id = "31e2c58d-22cc-496a-af6e-7fff3cf7d138",
                Name = "Andrii",
                Surname = "Donchenko",
                Patronymic = "Anatoliyovichh",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "your_admin@gmail.com",
                NormalizedEmail = "YOUR_ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpass"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<UserData>().HasData(new UserData
            {
                Id = "9cce296d-3729-4aae-ae5c-69f686a4f7d7",
                Name = "Didenko",
                Surname = "Denyis",
                Patronymic = "Oleksandrovich",
                UserName = "manager",
                NormalizedUserName = "MANAGER",
                Email = "your_manager@gmail.com",
                NormalizedEmail = "YOUR_MANAGER@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "passmanager"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "91f88ad6-fbdf-432e-a5cb-828e97def61d",
                UserId = "31e2c58d-22cc-496a-af6e-7fff3cf7d138"
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "e3ee9b61-b13d-409f-bcd6-cc0210b82cbd",
                UserId = "9cce296d-3729-4aae-ae5c-69f686a4f7d7"
            });

            modelBuilder.Entity<CarItem>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.CarItems)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.CarItem)
                .WithMany(c => c.OrderDetails)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
