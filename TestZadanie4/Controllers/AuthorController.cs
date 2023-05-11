using Microsoft.AspNetCore.Mvc;
using TestZadanie4.Models;
using TestZadanie4.Services;

namespace TestZadanie4.Controllers
{
    public class AuthorController : Controller
    {
        AuthorServices _authorServices;
        BookServices _bookServ;

        public AuthorController(AuthorServices authorServices, BookServices bookServ)
        {
            _authorServices = authorServices;
            _bookServ = bookServ;
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
                    return RedirectToAction("Index", "Home");
                }
                return View("Errors", response);
            }
            return View("Errors");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(Guid identificator)
        {
            if (ModelState.IsValid)
            {
                await _authorServices.Delete(identificator);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditAuthor(Guid identificator)
        {
            if (ModelState.IsValid)
            {
                var responce = _authorServices.Select().Result.FirstOrDefault(b => b.Id == identificator);
                if (responce != null)
                {
                    return View("EditAuthor", responce);
                }
                return View("Errors", responce);
            }
            return View("Errors");
        }


        [HttpPost]
        public async Task<IActionResult> EditAuthor(Author author)
        {

            if (ModelState.IsValid)
            {
                await _authorServices.Edit(author);
            }
            return RedirectToAction("Index", "Home");
        }

    
    }
}
