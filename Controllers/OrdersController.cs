using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index() // Ver órdenes
        {
            return View();
        }

        public IActionResult Create() // Crear orden
        {
            return View();
        }

        public IActionResult Details(int id) // Atender orden
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
