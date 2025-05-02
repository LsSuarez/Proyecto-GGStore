using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageProducts() // Gestionar productos
        {
            return View();
        }

        public IActionResult ManageUsers() // Gestionar usuarios
        {
            return View();
        }
    }
}
