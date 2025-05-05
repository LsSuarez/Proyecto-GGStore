using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using ProyectoGGStore.Data;
using ProyectoGGStore.Models;

namespace ProyectoGGStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor para inyectar el contexto de la base de datos
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción GET: Products/Index
public async Task<IActionResult> Index()
{
    var products = new List<Product>
    {
        new Product(1, "Teclado RGB", "Teclado mecánico RGB con retroiluminación", 59.99m, "1.jpg"),
        new Product(2, "PlayStation 5", "Consola PlayStation 5 de 2TB", 499.99m, "2.jpg"),
        new Product(3, "God of War Ragnarök", "Videojuego God of War Ragnarök", 59.99m, "3.jpg"),
        new Product(4, "Mouse Gaming", "Mouse con sensor de alta precisión", 39.99m, "4.jpg"),
        new Product(5, "Monitor 144Hz", "Monitor con alta frecuencia de actualización", 199.99m, "5.jpg"),
        new Product(6, "Producto de Imagen 6", "Descripción del producto", 99.99m, "6.jpg")
    };

    return View(products);
}


// Acción GET: Products/Details/5
public async Task<IActionResult> Details(int? id)
{
    if (id == null) // Validar si el id es nulo
    {
        return NotFound(); // Si el id es nulo, devuelve una página de error
    }

    // Busca el producto por id
    var product = await _context.Products
        .FirstOrDefaultAsync(m => m.Id == id);
    if (product == null) // Si el producto no existe, devuelve una página de error
    {
        return NotFound();
    }

    return View(product); // Pasa el producto a la vista para mostrarlo
}


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
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
=======

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
>>>>>>> WebCompleta
        }
    }
}
