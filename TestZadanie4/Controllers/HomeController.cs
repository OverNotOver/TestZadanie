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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

    }
}