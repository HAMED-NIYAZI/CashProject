using CashProject.Common;
using CashProject.Models;
using CashProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CashProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _cache;
        public CategoryController(ICategoryService categoryService,IMemoryCache cache)
        {
            _categoryService= categoryService;
            _cache= cache;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
             await _categoryService.InsertAsync(categoryViewModel);
             _cache.Remove(CacheKeys.CategoriesKey);
             return RedirectToAction("Index","Home");
        }
    }
}
