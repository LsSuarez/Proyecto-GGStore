using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
