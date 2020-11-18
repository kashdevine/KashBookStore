using KashBookStore.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DTOs
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Authors { get; set; }

        public void Load(Book book)
        {
            BookID = book.BookID;
            Title = book.Title;
            Price = book.Price;
            Authors = new Dictionary<int, string>();

            foreach(BookAuthor ba in book.BookAuthors)
            {
                Authors.Add(ba.AuthorID, ba.Author.FullName);
            }
        }
    }
}
