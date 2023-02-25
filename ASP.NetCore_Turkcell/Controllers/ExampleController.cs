using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCore_Turkcell.Controllers
{
    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class ExampleController : Controller
    {

        public IActionResult NoLayout()
        {
            return View();
        }
        public IActionResult Index2() 
        {
            var productList = new List<Products>()
            {
                new(){ID=1,Name="Kalem"},
                new(){ID=2,Name="Defter"},
                new(){ID=3,Name="Silgi"}
            };
            return View(productList);
        }
    }
}
