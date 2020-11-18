﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using KashBookStore.Models.DataLayer;
using KashBookStore.Models.DataLayer.Respositories;
using KashBookStore.Models.DomainModels;
using KashBookStore.Models.DTOs;
using KashBookStore.Models.ExtensionMethods;
using KashBookStore.Models.Grid;
using KashBookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KashBookStore.Controllers
{
    public class BookController : Controller
    {
        private BookStoreUnitOfWork data { get; set; }
        public BookController(BookstoreContext ctx) => data = new BookStoreUnitOfWork(ctx);

        public IActionResult Index() => RedirectToAction("List");

        //DTO has properties for the paging, soreting, and filtering route segments defined in teh Startup.cs file
        public ViewResult List(BookGridDTO values)
        {
            //get grid builder, which loads route segment values and stores them in session
            var builder = new BooksGridBuilder(HttpContext.Session, values, defaultSortFilter: nameof(Book.Title));

            //create a BookQueryOPtions object to build a query expresssion for a  page of data
            var options = new BookQueryOptions
            {
                Includes = "BookAuthors.Author, Genre",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            //call the SortFilter() method of the BookQueryOPtions object and pass it to the builder
            //object. It uses the route information and the properties of the builder object to 
            //add sort and filter options to the query expression.
            options.SortFilter(builder);

            //create view model and add page of book data, data for drop-downs,
            //the current route, and the total number of pages.
            var vm = new BookListViewModel
            {
                Books = data.Books.List(options),
                Authors = data.Authors.List(new QueryOptions<Author> { OrderBy = a => a.FirstName }),
                Genres = data.Genres.List(new QueryOptions<Genre> { OrderBy = g => g.Name }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Books.Count)
            };

            return View(vm);
        }

        public  ViewResult Details(int id)
        {
            var book = data.Books.Get(new QueryOptions<Book> 
            { 
             Includes="BookAuthors.Author, Genre",
             Where = b => b.BookID == id
            });

            return View(book);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false) 
        {
            //get current rout segments from session
            var builder = new BooksGridBuilder(HttpContext.Session);

            //clear or update filter route segment values. If update, get author data
            //from database so it can add author name slug to author filter value.
            if (clear)
                builder.ClearFilterSegments();
            else
            {
                var author = data.Authors.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, author);
            }

            //save route data back to session and redirect to Book/List action method,
            // passing dictionary of route sement values to build URL
            builder.SaveRouteSegment();
            return RedirectToAction("List", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult PageSize(int pagesize)
        {
            var builder = new BooksGridBuilder(HttpContext.Session);

            builder.CurrentRoute.PageSize = pagesize;
            builder.SaveRouteSegment();

            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
