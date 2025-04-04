using GGStoreProyecto.Data;
using GGStoreProyecto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GGStoreProyecto.Controllers
{
    [Authorize] // Asegura que todas las acciones requieren autenticación
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Mostrar contenido del carrito
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Si no hay un usuario autenticado, redirige al login
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Usuario");

            var items = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!items.Any())
            {
                TempData["Message"] = "Tu carrito está vacío. ¡Añade algunos productos!";
            }

            return View(items);
        }

        // ✅ Agregar producto al carrito
        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Si no hay un usuario autenticado, devolver un estado no autorizado
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Obtener el producto
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                TempData["Error"] = "Producto no encontrado";
                return RedirectToAction("Index");
            }

            // Verificar si el producto ya está en el carrito
            var existing = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

            if (existing != null)
            {
                // Si ya está en el carrito, incrementar la cantidad
                existing.Quantity += quantity;
            }
            else
            {
                // Si no está en el carrito, agregar un nuevo ítem
                var item = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserId = userId
                };

                _context.CartItems.Add(item);
            }

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync();
            TempData["Message"] = "Producto agregado al carrito!";
            return RedirectToAction("Index");
        }

        // ✅ Eliminar un producto del carrito
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Buscar el ítem a eliminar
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            // Si el ítem existe, eliminarlo
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Producto eliminado del carrito!";
            return RedirectToAction("Index");
        }

        // ✅ Vaciar todo el carrito
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Obtener todos los ítems del carrito del usuario
            var items = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            // Eliminar todos los ítems del carrito
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Carrito vaciado con éxito!";
            return RedirectToAction("Index");
        }
    }
}
