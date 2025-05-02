using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index() // Catálogo de Productos
        {
            return View();
        }

        public IActionResult Details(int id) // Ver detalles del producto
        {
            ViewBag.ProductId = id;
            return View();
        }
    }
}
