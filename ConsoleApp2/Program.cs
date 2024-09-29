using ConsoleApp2.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-");
        }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Airport;Integrated Security=True;Connect Timeout=2;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);

            
            modelBuilder.Entity<City>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<City>()
                .HasMany(c => c.Shops)
                .WithOne(s => s.City)
                .HasForeignKey(s => s.CityId);

            
            modelBuilder.Entity<Shop>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Shop>()
                .Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Workers)
                .WithOne(w => w.Shop)
                .HasForeignKey(w => w.ShopId);

            
            modelBuilder.Entity<Worker>()
                .Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Worker>()
                .Property(w => w.Surname)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Worker>()
                .Property(w => w.Email)
                .HasMaxLength(150);
            modelBuilder.Entity<Worker>()
                .Property(w => w.PhoneNumber)
                .HasMaxLength(15);
            modelBuilder.Entity<Worker>()
                .HasOne(w => w.Position)
                .WithMany(p => p.Workers)
                .HasForeignKey(w => w.PositionId);

            
            modelBuilder.Entity<Position>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.IsInStock)
                .IsRequired();
        }
    }
}

