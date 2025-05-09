using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class HomeController : Controller
    {
        // Método para servir la vista Index
        public IActionResult Index()
        {
            return View();
        }

        // Puedes agregar otros métodos para otras vistas, como Privacy
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
