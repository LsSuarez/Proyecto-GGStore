using Microsoft.AspNetCore.Mvc;
using GGStore.Models;
using GGStore.Data;
using Microsoft.EntityFrameworkCore;

namespace ProyectoGGStore.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor para inyectar el contexto de la base de datos
        public CatalogoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener los productos de la base de datos de forma asincrónica para mejorar el rendimiento
            var productos = await _context.Products.ToListAsync();

            // Enviar los productos a la vista
            return View(productos);
        }
    }
}
