using KashBookStore.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DataLayer.SeedData
{
    public class SeedBookAuthors : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasData(
                    new BookAuthor { BookID = 1, AuthorID = 18 },
                    new BookAuthor { BookID = 2, AuthorID = 20 },
                    new BookAuthor { BookID = 3, AuthorID = 7 },
                    new BookAuthor { BookID = 4, AuthorID = 2 },
                    new BookAuthor { BookID = 5, AuthorID = 19 },
                    new BookAuthor { BookID = 6, AuthorID = 8 },
                    new BookAuthor { BookID = 7, AuthorID = 12 },
                    new BookAuthor { BookID = 8, AuthorID = 16 },
                    new BookAuthor { BookID = 9, AuthorID = 2 },
                    new BookAuthor { BookID = 10, AuthorID = 20 },
                    new BookAuthor { BookID = 11, AuthorID = 15 },
                    new BookAuthor { BookID = 12, AuthorID = 4 },
                    new BookAuthor { BookID = 13, AuthorID = 21 },
                    new BookAuthor { BookID = 14, AuthorID = 5 },
                    new BookAuthor { BookID = 15, AuthorID = 9 },
                    new BookAuthor { BookID = 16, AuthorID = 13 },
                    new BookAuthor { BookID = 17, AuthorID = 7 },
                    new BookAuthor { BookID = 18, AuthorID = 4 },
                    new BookAuthor { BookID = 19, AuthorID = 11 },
                    new BookAuthor { BookID = 20, AuthorID = 22 },
                    new BookAuthor { BookID = 21, AuthorID = 17 },
                    new BookAuthor { BookID = 22, AuthorID = 3 },
                    new BookAuthor { BookID = 23, AuthorID = 14 },
                    new BookAuthor { BookID = 24, AuthorID = 1 },
                    new BookAuthor { BookID = 25, AuthorID = 10 },
                    new BookAuthor { BookID = 26, AuthorID = 6 },
                    new BookAuthor { BookID = 27, AuthorID = 23 },
                    new BookAuthor { BookID = 28, AuthorID = 4 },
                    new BookAuthor { BookID = 28, AuthorID = 26 },
                    new BookAuthor { BookID = 29, AuthorID = 25 }
                    );
        }
    }
}
