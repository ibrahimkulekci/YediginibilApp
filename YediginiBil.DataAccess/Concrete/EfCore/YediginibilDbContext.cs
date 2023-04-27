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

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient>  ProductIngredient { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<ProductBadge> ProductBadges { get; set; }
        public DbSet<Nutritive> Nutritives { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuLink> MenuLinks { get; set; }

    }
}
