using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class YediginibilDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-P79UT4Q;Database=YediginibilDB;integrated security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasKey(c => new { c.Id });
            //modelBuilder.Entity<Brand>()
            //    .HasKey(c => new { c.Id });
            //modelBuilder.Entity<Ingredient>()
            //    .HasKey(c => new { c.Id });
            //modelBuilder.Entity<ProductIngredient>()
            //    .HasKey(c => new { c.Id });

            //modelBuilder.Entity<ProductIngredient>(entity =>
            //{
            //    entity.HasOne(d => d.Ingredient)
            //        .WithMany(p => p.ProductIngredients)
            //        .HasForeignKey(d => d.IngredientId);

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.ProductIngredients)
            //        .HasForeignKey(d => d.ProductId);
            //});

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient>  ProductIngredient { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Blog> Blogs { get; set; }

    }
}
