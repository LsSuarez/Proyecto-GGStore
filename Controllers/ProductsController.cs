using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGGStore.Data;
using ProyectoGGStore.Models;

namespace ProyectoGGStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products/Index
        public async Task<IActionResult> Index()
        {
            // Obtiene todos los productos de la base de datos
            var products = await _context.Products.ToListAsync();
            return View(products); // Devuelve la vista con la lista de productos
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Validar si el id es nulo
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) // Validar si el producto existe
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) // Validar si el producto existe
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Redirige a la acción Index después de eliminar el producto
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id); // Verifica si el producto existe
        }
    }
}
