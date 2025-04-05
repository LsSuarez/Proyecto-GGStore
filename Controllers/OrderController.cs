using GGStoreProyecto.Data;
using GGStoreProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GGStoreProyecto.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor que recibe el contexto de base de datos
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Vista para crear una nueva orden
        public IActionResult Create()
        {
            return View();
        }

        // Lógica de creación de la orden (POST)
        [HttpPost]
        [ValidateAntiForgeryToken] // Protección contra CSRF
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                // Obtener el ID del usuario autenticado
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Si el usuario no está autenticado, redirige a la página de login
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Usuario");
                }

                // Obtener los productos en el carrito del usuario
                var cartItems = await _context.CartItems
                    .Include(c => c.Product)
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                // Si no hay productos en el carrito, redirige a la página principal
                if (!cartItems.Any())
                {
                    return RedirectToAction("Index", "Home");
                }

                // Establecer los detalles de la orden
                order.UserId = userId;
                order.OrderDate = System.DateTime.Now;

                // Calcular el precio total de la orden, asegurándose de que Product no sea nulo
                order.TotalPrice = cartItems.Where(item => item.Product != null)
                    .Sum(item => GetPrice(item.Product, item.Quantity));

                // Agregar la orden a la base de datos
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Crear los detalles de la orden, asegurándose de que Product no sea nulo
                var orderDetails = cartItems.Where(item => item.Product != null)
                    .Select(item => new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = GetPrice(item.Product, item.Quantity)
                    }).ToList();

                // Agregar los detalles de la orden a la base de datos
                _context.OrderDetails.AddRange(orderDetails);
                await _context.SaveChangesAsync();

                // Vaciar el carrito del usuario después de crear la orden
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                // Redirigir a la página de confirmación o detalles de la orden
                return RedirectToAction("Details", "Order", new { id = order.Id });
            }

            // Si la validación falla, volver a mostrar la vista de creación de la orden
            return View(order);
        }

        // Método para obtener el precio de un producto
        private decimal GetPrice(Product product, int quantity)
        {
            if (product != null)
            {
                return product.Price * quantity;
            }
            return 0;
        }

        // Vista para mostrar los detalles de la orden
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}