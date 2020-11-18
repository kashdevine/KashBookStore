using KashBookStore.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DataLayer.Respositories
{
    public class BookStoreUnitOfWork : IBookStoreUnitOfWork
    {
        private BookstoreContext context;
        private Repository<Book> bookData;
        private Repository<Author> authorData;
        private Repository<BookAuthor> bookAuthorData;
        private Repository<Genre> genreData;

        public BookStoreUnitOfWork(BookstoreContext ctx)
        {
            context = ctx;
        }

        //create get methods that check for null values
        public Repository<Book> Books 
        {
            get
            {
                if (bookData == null)
                    bookData = new Repository<Book>(context);
                return bookData;
            }
        }

        public Repository<Author> Authors 
        {
            get
            {
                if (authorData == null)
                    authorData = new Repository<Author>(context);
                return authorData;
            }
        }

        public Repository<BookAuthor> BookAuthors 
        {
            get
            {
                if (bookAuthorData == null)
                    bookAuthorData = new Repository<BookAuthor>(context);
                return bookAuthorData;
            }
        }

        public Repository<Genre> Genres 
        {
            get
            {
                if (genreData == null)
                    genreData = new Repository<Genre>(context);
                return genreData;
            }
        }



        public void AddNewBookAuthors(Book book, int[] authorids)
        {
            book.BookAuthors = authorids.Select(i => new BookAuthor { Book = book, AuthorID = i }).ToList();
        }

        public void DeleteCurrentBookAuthors(Book book)
        {
            var currentAuthors = BookAuthors.List(new QueryOptions<BookAuthor>
            {
                Where = ba => ba.BookID == book.BookID
            });

            foreach(BookAuthor ba in currentAuthors)
            {
                BookAuthors.Delete(ba);
            }
        }

        public void Save() => context.SaveChanges();
    }
}
