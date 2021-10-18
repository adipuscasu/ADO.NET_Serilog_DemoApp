using ADO.NET_Demo.Web.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.NET_Demo.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IBooksRepo _booksRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IBooksRepo booksRepo,
            ILogger<HomeController> logger
            )
        {
            _booksRepo = booksRepo ?? throw new ArgumentNullException(nameof(booksRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        // GET: BooksController
        public async Task<ActionResult> IndexAsync()
        {
            var books = await _booksRepo.FetchBooks();
            ViewData["Books"] = books;
            ViewBag.Books = books;
            _logger.LogError($"Getting {books.ToList().Count} books");
            return View();
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BooksController/Create
        [Authorize]

        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Edit/5
        [Authorize]

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        [Authorize]

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
