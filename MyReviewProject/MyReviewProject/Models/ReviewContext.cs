using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class ReviewContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Category> Categories { get; set;}

        public DbSet<SubCategory> SubCategories { get; set; }

        //public DbSet<ApplicationUser> AspUsers { get; set; }

        //public DbSet<AnonUser> AnonUsers { get; set; }

        //public DbSet<User> Users { get; set; }

        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>().ToTable("AspUsers");
        //}

        //public DbQuery<T> Query<T>() where T : class
        //{
        //    return Set<T>().AsNoTracking();
        //}
    }

    
}