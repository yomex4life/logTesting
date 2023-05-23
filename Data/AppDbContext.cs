using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace logTesting.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Article>().HasOne(a => a.Author).WithMany(a => a.Articles).HasForeignKey(a => a.AuthorId);
            //modelBuilder.Entity<Article>().HasOne(a => a.Category).WithMany(a => a.Articles).HasForeignKey(a => a.CategoryId);
            modelBuilder.Entity<Author>().HasMany(a => a.Articles).WithOne(a => a.Author).HasForeignKey(a => a.AuthorId);
            modelBuilder.Entity<Category>().HasMany(a => a.Articles).WithOne(a => a.Category).HasForeignKey(a => a.CategoryId);
            
        }
    }
}