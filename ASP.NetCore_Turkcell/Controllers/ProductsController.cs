using ASP.NetCore_Turkcell.Helpers;
using ASP.NetCore_Turkcell.Models;
using ASP.NetCore_Turkcell.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NetCore_Turkcell.Controllers
{
    public class ProductsController : Controller
    {
        
        private AppDbContext _context;

        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            //DI Container
            //Dependency Injection Pattern
            _productRepository = new ProductRepository();
            _context = context;
            _mapper = mapper;

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem 1", Price = 150, Stock = 100, Color = "Red" });
            //    _context.Products.Add(new Product() { Name = "Kalem 2", Price = 100, Stock = 200, Color = "Green" });
            //    _context.Products.Add(new Product() { Name = "Kalem 3", Price = 175, Stock = 300, Color = "Brown" });
            //}
            //_context.SaveChanges();

        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() { 
                new() { Data="Mavi", Value="Mavi"},
                new() { Data="Kırmızı", Value="Kırmızı"},
                new() { Data="Sarı", Value="Sarı"},

            },"Value","Data");
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new() { Data="Mavi", Value="Mavi"},
                new() { Data="Kırmızı", Value="Kırmızı"},
                new() { Data="Sarı", Value="Sarı"},

            }, "Value", "Data");

            if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            {
                ModelState.AddModelError(String.Empty, "Ürün ismi A harfi ile başlayamaz");

            }

            if (ModelState.IsValid)
            {

                try
                {
                    
                    TempData["status"] = "Ürün başarıyla güncellendi";
                    _context.Products.Add(_mapper.Map<Product>(newProduct));
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {

                    ModelState.AddModelError(String.Empty, "Ürün kaydedilirken bir hata meydana geldi.Lütfen daha sonra tekrar deneyiniz.");
                    return View();
                }

            }
            else
            {

                return View();
            }
           

            
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            ViewBag.ExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new() { Data="Mavi", Value="Mavi"},
                new() { Data="Kırmızı", Value="Kırmızı"},
                new() { Data="Sarı", Value="Sarı"},

            }, "Value", "Data",product.Color);
            return View(_mapper.Map<ProductViewModel>(product)); 
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel updateProduct)
        {
            

            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = updateProduct.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1 },
                {"3 Ay",3 },
                {"6 Ay",6 },
                {"12 Ay",12 },
            };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {
                new() { Data="Mavi", Value="Mavi"},
                new() { Data="Kırmızı", Value="Kırmızı"},
                new() { Data="Sarı", Value="Sarı"},

            }, "Value", "Data", updateProduct.Color);

                return View();  

            }
            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla güncellendi";
            return RedirectToAction("Index");


        }
        [AcceptVerbs ("GET","POST")]
        public IActionResult HasProductName(string Name) // aynı ürün veri tabanında var mı yok mu onu kontrol eden metod. bu bir java metodudur.
        {
            var anyproduct = _context.Products.Any(Product => Product.Name.ToLower() == Name.ToLower());
            if(anyproduct)
            {
                return Json("Kaydetmeye çalıştığınız ürün ismi veri tabanında bulunmaktadır.");
            }
            else
                {
                    return Json(true);
                }
            
        }

    }
}
