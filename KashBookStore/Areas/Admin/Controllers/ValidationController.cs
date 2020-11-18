using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KashBookStore.Areas.Admin.Models;
using KashBookStore.Models.DataLayer;
using KashBookStore.Models.DataLayer.Respositories;
using KashBookStore.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KashBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<Author> authorData { get; set; }
        private Repository<Genre> genreData { get; set; }

        public ValidationController(BookstoreContext ctx)
        {
            authorData = new Repository<Author>(ctx);
            genreData = new Repository<Genre>(ctx);
        }

        public JsonResult CheckGenre(string genreID)
        {
            var validate = new Validate(TempData);
            validate.CheckGenre(genreID, genreData);

            if (validate.IsValid)
            {
                validate.MarkGenreChecked();
                return Json(true);
            }
            else
                return Json(validate.ErrorMessage);
        }

        public JsonResult CheckAuthor(string firstName, string lastName, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckAuthor(firstName, lastName, operation, authorData);
            if (validate.IsValid)
            {
                validate.MarkAuthorChecked();
                return Json(true);
            }
            else
                return Json(validate.ErrorMessage);
        }
    }
}
