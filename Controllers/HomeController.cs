using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookstore.Models;
using Bookstore.Models.ViewModels;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }
        // controller for displaying available books
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int numResults = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == bookCategory || bookCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * numResults)
                .Take(numResults),

                // page info controller
                PageInfo = new PageInfo
                {
                    TotalNumBooks = 
                        (bookCategory == null 
                            ? repo.Books.Count()
                            : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = numResults,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

    }
}
