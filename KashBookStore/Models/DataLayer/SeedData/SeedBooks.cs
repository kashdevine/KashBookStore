using KashBookStore.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DataLayer.SeedData
{
    public class SeedBooks : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                    new Book { BookID = 1, Title = "1776", GenreID = "history", Price = 18.00 },
                    new Book { BookID = 2, Title = "1984", GenreID = "scifi", Price = 5.50 },
                    new Book { BookID = 3, Title = "And Then There Were None", GenreID = "mystery", Price = 4.50 },
                    new Book { BookID = 4, Title = "Band of Brothers", GenreID = "history", Price = 11.50 },
                    new Book { BookID = 5, Title = "Beloved", GenreID = "novel", Price = 10.99 },
                    new Book { BookID = 6, Title = "Between the World and Me", GenreID = "memoir", Price = 13.50 },
                    new Book { BookID = 7, Title = "Bossypants", GenreID = "memoir", Price = 4.25 },
                    new Book { BookID = 8, Title = "Brave New World", GenreID = "scifi", Price = 16.25 },
                    new Book { BookID = 9, Title = "D-Day", GenreID = "history", Price = 15.00 },
                    new Book { BookID = 10, Title = "Down and Out in Paris and London", GenreID = "memoir", Price = 12.50 },
                    new Book { BookID = 11, Title = "Dune", GenreID = "scifi", Price = 8.75 },
                    new Book { BookID = 12, Title = "Emma", GenreID = "novel", Price = 9.00 },
                    new Book { BookID = 13, Title = "Frankenstein", GenreID = "scifi", Price = 6.50D },
                    new Book { BookID = 14, Title = "Go Tell it on the Mountain", GenreID = "novel", Price = 10.25 },
                    new Book { BookID = 15, Title = "Guns, Germs, and Steel", GenreID = "history", Price = 15.50 },
                    new Book { BookID = 16, Title = "Hunger", GenreID = "memoir", Price = 14.50 },
                    new Book { BookID = 17, Title = "Murder on the Orient Express", GenreID = "mystery", Price = 6.75 },
                    new Book { BookID = 18, Title = "Pride and Prejudice", GenreID = "novel", Price = 8.50 },
                    new Book { BookID = 19, Title = "Rebecca", GenreID = "mystery", Price = 10.99 },
                    new Book { BookID = 20, Title = "The Art of War", GenreID = "history", Price = 5.75 },
                    new Book { BookID = 21, Title = "The Girl with the Dragon Tattoo", GenreID = "mystery", Price = 8.50 },
                    new Book { BookID = 22, Title = "The Handmaid's Tale", GenreID = "scifi", Price = 12.50 },
                    new Book { BookID = 23, Title = "The Maltese Falcon", GenreID = "mystery", Price = 10.99 },
                    new Book { BookID = 24, Title = "The New Jim Crow", GenreID = "history", Price = 13.75 },
                    new Book { BookID = 25, Title = "The Year of Magical Thinking", GenreID = "memoir", Price = 13.50 },
                    new Book { BookID = 26, Title = "Wuthering Heights", GenreID = "novel", Price = 9.00 },
                    new Book { BookID = 27, Title = "Running With Scissors", GenreID = "memoir", Price = 11.00 },
                    new Book { BookID = 28, Title = "Pride and Prejudice and Zombies", GenreID = "novel", Price = 8.75 },
                    new Book { BookID = 29, Title = "Harry Potter and the Sorcerer's Stone", GenreID = "novel", Price = 9.75 }
                );
        }
    }
}
