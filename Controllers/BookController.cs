using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMvcTest.Repositories.Abstract;
using CoreMvcTest.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMvcTest.Models
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepos;
        private readonly IBookCategoryRepository _bookCategoryRepos;
       // private readonly IEnumerable<BookCategory> bookCategories;
        public BookController(IBookRepository bookRepos,IBookCategoryRepository bookCategoryRepos)
        {
            _bookCategoryRepos = bookCategoryRepos;
            _bookRepos = bookRepos;
        }
        private  async  Task<IEnumerable<SelectListItem>> GetCategorySelectList(int selectedId = 0)
        {
            var bookCategories = await _bookCategoryRepos.GetAll();
            var data=bookCategories.Select(category => new SelectListItem { Text = category.CategoryName, Value = category.Id.ToString(),Selected=category.Id==selectedId });
            return data;
        }
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Book> books = await _bookRepos.GetAll();
            return View(books);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _bookRepos.Delete(id);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Add()
        {
            var book = new Book();
            //fetching BookCategory list below
            book.BookCategoryList = await GetCategorySelectList();
            book.PublishDate = DateTime.Now;
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            book.BookCategoryList = await GetCategorySelectList(book.BookCategory_Id);
            if (!ModelState.IsValid)
                return View(book);
            bool result = await _bookRepos.AddUpdate(book);
            ViewBag.msg = result ? "Added Successfully" : "Error occured";
            if (result)
            {
                //Empty validations here
                ModelState.Clear();
            }
            return View(book);
        }

        public  async Task<IActionResult> Edit(int id)
        {
            Book book = await _bookRepos.GetById(id);
            book.BookCategoryList = await GetCategorySelectList(book.BookCategory_Id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            book.BookCategoryList = await GetCategorySelectList(book.BookCategory_Id);
            if (!ModelState.IsValid)
                return View(book);
            bool result = await _bookRepos.AddUpdate(book);
            ViewBag.msg = result ? "Added Successfully" : "Error occured";
            if (result)
                return RedirectToAction("GetAll");
            return View(book);
        }
    }
}
