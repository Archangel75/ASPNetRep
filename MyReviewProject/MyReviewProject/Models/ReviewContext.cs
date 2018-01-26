using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DbSet<News> News { get; set; }

        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }
    }

    
}