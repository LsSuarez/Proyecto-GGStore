using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
