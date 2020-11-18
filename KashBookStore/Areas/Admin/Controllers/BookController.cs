﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KashBookStore.Areas.Admin.Models;
using KashBookStore.Models.DataLayer;
using KashBookStore.Models.DataLayer.Respositories;
using Microsoft.AspNetCore.Mvc;
using KashBookStore.Models.DomainModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;

namespace KashBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {

        private BookStoreUnitOfWork data { get; set; }
        public BookController(BookstoreContext ctx) => data = new BookStoreUnitOfWork(ctx);

        public IActionResult Index()
        {
            //clear all previous searches
            var search = new SearchData(TempData);
            search.Clear();
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetBook(id, "Edit");

        [HttpPost]
        public IActionResult Edit(BookViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.DeleteCurrentBookAuthors(vm.Book);
                data.AddNewBookAuthors(vm.Book, vm.SelectedAuthors);
                data.Books.Update(vm.Book);
                data.Save();
                TempData["message"] = $"{vm.Book.Title} was updated";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("Book", vm);
            }
        }

        [HttpGet]
        public ViewResult Add(int id) => GetBook(id, "add");

        [HttpPost]
        public IActionResult Add(BookViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.AddNewBookAuthors(vm.Book, vm.SelectedAuthors);
                data.Books.Insert(vm.Book);
                data.Save();

                TempData["message"] = $"{vm.Book.Title} was added to Books";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "add");
                return View("Book", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetBook(id, "Delete");

        [HttpPost]
        public IActionResult Delete(BookViewModel vm)
        {
            //no ModelState.IsValid check here bc there's no user input-
            //only BookID in hidden field is posted from form.
            data.Books.Delete(vm.Book); //cascading delete will get BookAuthors
            data.Save();
            TempData["message"] = $"{vm.Book.Title} was deleted";
            return RedirectToAction("Search");
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Book>
                {
                    Includes = "Genre, BookAuthors.Author"
                };

                if (search.IsBook)
                {
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for the book title '{vm.SearchTerm}'";
                }

                if (search.IsAuthor)
                {
                    //if there's no space, search both first and last name by search term.
                    //Otherwise, assume there's a first and last name and refine search.
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1) //no space
                    {
                        options.Where = b => b.BookAuthors.Any(
                             ba => ba.Author.FirstName.Contains(vm.SearchTerm) ||
                             ba.Author.LastName.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        //assume first and last name
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1); // skip space
                        options.Where = b => b.BookAuthors.Any(
                            ba => ba.Author.FirstName.Contains(first) &&
                            ba.Author.LastName.Contains(last));
                    }

                    vm.Header = $"Search results for author '{vm.SearchTerm}'";
                }

                if (search.IsGenre)
                {
                    options.Where = b => b.GenreID.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for the genreID {vm.SearchTerm}";
                }

                vm.Books = data.Books.List(options);
                return View("SearchResults", vm);
            }
            else
                return View("Index");
        }

        private object SearchData(ITempDataDictionary tempData)
        {
            throw new NotImplementedException();
        }

        // search (posted from search text box in Index page). Search term is required field.
        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };

                return RedirectToAction("Search");
            }
            else
                return RedirectToAction("Index");
        }

        private ViewResult GetBook(int id, string operation)
        {
            var book = new BookViewModel();
            Load(book, operation, id);

            return View("Book", book);
        }

        private void Load(BookViewModel vm, string op, int? id = null) 
        {
            if (Operation.IsAdd(op))
            {
                vm.Book = new Book();
            }
            else
            {
                vm.Book = data.Books.Get(new QueryOptions<Book>
                {
                    Includes = "BookAuthors.Author, Genre",
                    Where = b => b.BookID == (id ?? vm.Book.BookID)
                });
            }

            vm.SelectedAuthors = vm.Book.BookAuthors?.Select(ba => ba.Author.AuthorID).ToArray();
            vm.Authors = data.Authors.List(new QueryOptions<Author> { OrderBy = a => a.FirstName });
            vm.Genres = data.Genres.List(new QueryOptions<Genre> { OrderBy = g => g.Name });
        }
    }
}
