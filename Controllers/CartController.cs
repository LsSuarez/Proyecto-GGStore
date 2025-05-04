using Microsoft.AspNetCore.Mvc;

namespace GGStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index() // Ver Carrito
        {
            return View();
        }

        public IActionResult Checkout() // Pagar
        {
            return View();
        }
    }
}
