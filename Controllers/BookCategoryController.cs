using System;
using System.Threading.Tasks;
using CoreMvcTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CoreMvcTest.Repositories.Abstract;

namespace CoreMvcTest.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly IConfiguration _configuration;
        private string baseUrl;
        private readonly IBookCategoryRepository _categoryRepos;
        public BookCategoryController(IConfiguration configuration, IBookCategoryRepository categoryRepos)
        {
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("baseUrl")+"/bookcategory";
            _categoryRepos = categoryRepos;
        }
        public async Task<IActionResult> GetAll()
        {
            var data = await _categoryRepos.GetAll();
            return View(data);  
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookCategory model)
        {
            if (!ModelState.IsValid)
            {
               return model.Id > 0 ? View(nameof(Edit), model) : View(nameof(Add), model);
            }
            bool result = await _categoryRepos.AddUpdate(model);
            if (result)
            {
                TempData["msg"] = "Saved successfully";
                return model.Id>0?RedirectToAction(nameof(GetAll)): RedirectToAction(nameof(Add));
            }
            return model.Id>0?View(nameof(Edit),model): View(nameof(Add),model);

        }

        public async Task<IActionResult> Edit(int id)
        {
                var bookCategory = await _categoryRepos.FindById(id);
                if (bookCategory == null)
                    return NotFound();
                return View(bookCategory);           
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await  _categoryRepos.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}
