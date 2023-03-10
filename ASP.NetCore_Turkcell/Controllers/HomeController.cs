using ASP.NetCore_Turkcell.Helpers;
using ASP.NetCore_Turkcell.Models;
using ASP.NetCore_Turkcell.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.NetCore_Turkcell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;



        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,

            }).ToList();
            ViewBag.productListPartialViewModel= new ProductListPartialViewModel()
            { 
            Products=products
            };
            return View();
        }

        public IActionResult Privacy()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,

            }).ToList();
            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}