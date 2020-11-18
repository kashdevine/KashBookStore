using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AuthorController : Controller
    {
        private Repository<Author> data { get; set; }

        public AuthorController(BookstoreContext ctx) => data = new Repository<Author>(ctx);
 
        
        public IActionResult Index() => RedirectToAction("List");

        //dto has properties for the paging and sorting route segments defined in the Startup.cs file
        public ViewResult List(GridDTO vals)
        {
            //get grid builder, which loads route segment values and stores them in session
            string defaultSort = nameof(Author.LastName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegment();

            //create options for querying authors. OrderBy depends on valin in Sortfield route
            var options = new QueryOptions<Author>
            {
                Includes = "BookAuthors.Book",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };

            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.LastName;
            else
                options.OrderBy = a => a.FirstName;

            //create view model and add page of author data, the current route,
            //and the total number of pages.

            var vm = new AuthorListViewModel
            {
                Authors = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);

        }
        public ViewResult Details(int id)
        {
            var author = data.Get(new QueryOptions<Author>
            {

                Includes = "BookAuthors.Book",
                Where = a => a.AuthorID == id

            });

            return View(author);
        }
    }
}
