using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestZadanie4.Models;
using TestZadanie4.Services;

namespace TestZadanie4.Controllers
{
    public class BookController : Controller
    {
        BookServices _bookServ;


        public BookController(BookServices bookServ)
        {
            _bookServ = bookServ;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateBook()
        {
            var books = await _bookServ.Select();
            return View(books);
        }



        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
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

        [HttpPost]
        public async Task<IActionResult> CreateBookAjax(Book book)
        {
            if (ModelState.IsValid)
            {
                var response =  await _bookServ.CreateBook(book);
                if (response.Status == ResultCode.OK)
                {
                    return Ok("Книга добавлена в базу!");
                }
            }
            return Ok("Книга не додана");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBook(Guid identificator)
        {
            if (ModelState.IsValid)
            {
                await _bookServ.Delete(identificator);
            }
            return RedirectToAction("CreateBook");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(Guid identificator)
        {
            if (ModelState.IsValid)
            {
                var responce = _bookServ.Select().Result.FirstOrDefault(b => b.Id == identificator);
                if (responce != null)
                {
                    return View("UpdateBook", responce);
                }
                return View("Errors", responce);
            }
            return View("Errors");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBook(Book book)
        {

            if (ModelState.IsValid)
            {
                await _bookServ.Update(book);
            }
            return RedirectToAction("CreateBook");
        }



    }
}
