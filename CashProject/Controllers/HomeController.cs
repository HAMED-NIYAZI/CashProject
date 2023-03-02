using CashProject.Common;
using CashProject.Models;
using CashProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CashProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _cache;
        private readonly ICategoryService _categoriyService;
        public HomeController(ILogger<HomeController> logger, IMemoryCache cache, ICategoryService categoryService)
        {
            _logger = logger;
            _cache = cache;
            _categoriyService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            var cats = _cache.Get<List<CategoryViewModel>>(CacheKeys.CategoriesKey);
            if (cats==null)
            {
                 cats = _categoriyService.GetAllAsync().Result;
                _cache.Set<List<CategoryViewModel>>(CacheKeys.CategoriesKey, cats,DateTimeOffset.Now.AddDays(1));
            }
            ViewBag.Categories = cats;


            //List<CategoryViewModel>? categories = await _cache.GetOrCreateAsync<List<CategoryViewModel>>(CacheKeys.CategoriesKey,
            //      entry =>
            //    {
            //       // entry.SlidingExpiration = TimeSpan.FromSeconds(15);
            //       entry.AbsoluteExpiration=DateTime.Now.AddDays(1);
            //        return _categoriyService.GetAllAsync();
            //    }
            //);

            //ViewBag.Categories = categories;


            return View();
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