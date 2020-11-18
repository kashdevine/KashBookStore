using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KashBookStore.Models.DomainModels
{
    public class Book
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Please enter a book title.")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.0, 1000000.0, ErrorMessage ="Price must be between 0 and 1 million.")]
        public double Price { get; set; }

        [Required(ErrorMessage ="Please select genre")]
        public string GenreID { get; set; }
        public Genre Genre { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

    }
}
