using KashBookStore.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DataLayer.SeedData
{
    public class SeedGenres : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { GenreID = "novel", Name = "Novel" },
                new Genre { GenreID = "memoir", Name = "Memoir" },
                new Genre { GenreID = "mystery", Name = "mystery" },
                new Genre { GenreID = "scifi", Name = "Science Fiction" },
                new Genre { GenreID = "history", Name = "History" }
                );
        }
    }
}
