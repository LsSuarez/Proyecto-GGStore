using Microsoft.AspNetCore.Mvc;

namespace GGStoreProyecto.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}