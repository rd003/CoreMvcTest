using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMvcTest.Models;
using CoreMvcTest.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMvcTest.Controllers
{
    public class BookPurchaseController : Controller
    {
        private readonly IBookPurchaseRepository _bookPurchaseRepository;
        private readonly IBookRepository _bookRepository;
        public BookPurchaseController(IBookRepository bookRepository,IBookPurchaseRepository bookPurchaseRepository)
        {
            _bookPurchaseRepository = bookPurchaseRepository;
            _bookRepository = bookRepository;
        }

        private async Task<IEnumerable<SelectListItem>> GetBookList(int selectedId = 0)
        {
            var books = await _bookRepository.GetAll();
            return books.Select(a => new SelectListItem { Text = a.BookName, Value = a.Id.ToString(), Selected = a.Id == selectedId }).ToList();
        }
        public async Task<IActionResult> GetAll()
        {
            var data = await _bookPurchaseRepository.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _bookPurchaseRepository.Delete(id);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Add()
        {
            var model = new BookPurchase();
            model.BookList = await GetBookList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookPurchase model)
        {
            model.BookList = await GetBookList(model.Id);
            if (!ModelState.IsValid)
                return View(model);
            var result = await _bookPurchaseRepository.AddUpdate(model);
            if(result)
            {
                TempData["msg"] = "Data has saved";
                return RedirectToAction("Add");
            }
            else
            {
                TempData["msg"] = "Error has occured";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _bookPurchaseRepository.FindById(id);
            model.BookList = await GetBookList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookPurchase model)
        {
            model.BookList = await GetBookList(model.Id);
            if (!ModelState.IsValid)
                return View(model);
            var result = await _bookPurchaseRepository.AddUpdate(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                TempData["msg"] = "Error has occured";
                return View(model);
            }
        }

    }
}
