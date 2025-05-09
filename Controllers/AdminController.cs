using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    [Authorize(Roles = "Admin")]  // Asegura que solo los usuarios con rol 'Admin' accedan a este controlador
    public class AdminController : Controller
    {
        // Acción para la página principal del administrador
        public IActionResult Index()
        {
            return View();
        }

        // Acción para gestionar productos
        public IActionResult ManageProducts()
        {
            return View();
        }

        // Acción para gestionar usuarios
        public IActionResult ManageUsers()
        {
            return View();
        }
    }
}
