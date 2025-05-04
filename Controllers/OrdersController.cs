using Microsoft.AspNetCore.Mvc;
using ProyectoGGStore.Models;
using ProyectoGGStore.Data;

namespace ProyectoGGStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción Index para mostrar todas las órdenes
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders); // Asegúrate de que exista la vista Index.cshtml
        }

        // Acción Create para crear una nueva orden
        public IActionResult Create()
        {
            return View(); // Asegúrate de que exista la vista Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirige al Index después de crear la orden
            }

            return View(order);
        }
    }
}
