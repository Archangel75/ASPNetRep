using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SimpleBookStore.Models
{
    public class BookContext: DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

    }

    public class BookInitializer: DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            context.Books.Add(new Book { Id = 1, Name = "451 farengheit", Author = "Bredberry", Price = 100 });
            context.Books.Add(new Book { Id = 2, Name = "Vine from something", Author = "Bredberry", Price = 100 });
            context.Books.Add(new Book { Id = 3, Name = "Mor, pupil of Death", Author = "Bredberry", Price = 100 });

            base.Seed(context);
        }
    }
}