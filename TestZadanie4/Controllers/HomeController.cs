using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.ComponentModel;
using System.Diagnostics;
using TestZadanie4.Models;
using TestZadanie4.Models.Db;
using TestZadanie4.Services;

namespace TestZadanie4.Controllers

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        BookServices _bookServ;
        AuthorServices _authorServices;



        public HomeController(ILogger<HomeController> logger, BookServices bookServices, AuthorServices authorServices)
        {
            _logger = logger;
            _bookServ = bookServices;
            _authorServices = authorServices;
        }

        public async Task <IActionResult> Index()
        {
            var authors = await _authorServices.Select();
            return View(authors);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> AddBook()
        {
            var books = await _bookServ.Select();
            return View(books);
        }

     

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookServ.CreateBook(book);
                if (response.Status == ResultCode.OK)
                {
                    return View(response.Data);
                }
                return View("Errors", response);
            }
            return View("Errors");
        }
        public async Task<IActionResult> AddAuthor()
        {
            Author authorNew = new Author();
            var books = await _bookServ.Select();
            authorNew.Books = books;
            return View(authorNew);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                var response = await _authorServices.CreateAuthor(author);
                if (response.Status == ResultCode.OK)
                {
                    return RedirectToAction("Index");
                }
                return View("Errors", response);
            }
            return View("Errors");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteBook(Guid identificator)
        {
            if (ModelState.IsValid)
            {
                await _bookServ.Delete(identificator);
            }
            return RedirectToAction("AddBook");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(Guid identificator)
        {
            if (ModelState.IsValid)
            {
                await _authorServices.Delete(identificator);
            }
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}