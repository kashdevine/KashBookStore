using KashBookStore.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DataLayer.SeedData
{
    public class SeedAuthors : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                  new Author { AuthorID = 1, FirstName = "Michelle", LastName = "Alexander" },
                  new Author { AuthorID = 2, FirstName = "Stephen E.", LastName = "Ambrose" },
                  new Author { AuthorID = 3, FirstName = "Margaret", LastName = "Atwood" },
                  new Author { AuthorID = 4, FirstName = "Jane", LastName = "Austen" },
                  new Author { AuthorID = 5, FirstName = "James", LastName = "Baldwin" },
                  new Author { AuthorID = 6, FirstName = "Emily", LastName = "Bronte" },
                  new Author { AuthorID = 7, FirstName = "Agatha", LastName = "Christie" },
                  new Author { AuthorID = 8, FirstName = "Ta-Nehisi", LastName = "Coates" },
                  new Author { AuthorID = 9, FirstName = "Jared", LastName = "Diamond" },
                  new Author { AuthorID = 10, FirstName = "Joan", LastName = "Didion" },
                  new Author { AuthorID = 11, FirstName = "Daphne", LastName = "Du Maurier" },
                  new Author { AuthorID = 12, FirstName = "Tina", LastName = "Fey" },
                  new Author { AuthorID = 13, FirstName = "Roxane", LastName = "Gay" },
                  new Author { AuthorID = 14, FirstName = "Dashiel", LastName = "Hammett" },
                  new Author { AuthorID = 15, FirstName = "Frank", LastName = "Herbert" },
                  new Author { AuthorID = 16, FirstName = "Aldous", LastName = "Huxley" },
                  new Author { AuthorID = 17, FirstName = "Stieg", LastName = "Larsson" },
                  new Author { AuthorID = 18, FirstName = "David", LastName = "McCullough" },
                  new Author { AuthorID = 19, FirstName = "Toni", LastName = "Morrison" },
                  new Author { AuthorID = 20, FirstName = "George", LastName = "Orwell" },
                  new Author { AuthorID = 21, FirstName = "Mary", LastName = "Shelley" },
                  new Author { AuthorID = 22, FirstName = "Sun", LastName = "Tzu" },
                  new Author { AuthorID = 23, FirstName = "Augusten", LastName = "Burroughs" },
                  new Author { AuthorID = 25, FirstName = "JK", LastName = "Rowling" },
                  new Author { AuthorID = 26, FirstName = "Seth", LastName = "Grahame-Smith" }
                  );
        }
    }
}
