using KashBookStore.Models.DomainModels;
using KashBookStore.Models.ExtensionMethods;
using KashBookStore.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DataLayer
{
    //Extends generic QueryOPtions<Book> class to add a
    //SortFilter() method that adds the sort and filter
    //code specific to the bookstore application
    public class BookQueryOptions : QueryOptions<Book>
    {
        public void SortFilter(BooksGridBuilder builder)
        {
            //filter
            if (builder.IsFilterByGenre)
                Where = b => b.GenreID == builder.CurrentRoute.GenreFilter;
            if (builder.IsFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under7")
                    Where = b => b.Price < 7;
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                    Where = b => b.Price >= 7 && b.Price <= 14;
                else
                    Where = b => b.Price > 14;
            }

            if (builder.IsFilterByAuthor)
            {
                int id = builder.CurrentRoute.AuthorFilter.ToInt();
                //to filter the books by author, use the LINQ Any() method.
                if (id > 0)
                    Where = b => b.BookAuthors.Any(ba => ba.AuthorID == id);
            }

            //Sort
            if (builder.IsSortByGenre)
                OrderBy = b => b.Genre.Name;
            else if (builder.IsSortByPrice)
                OrderBy = b => b.Price;
            else
                OrderBy = b => b.Title;
        }
    }
}
