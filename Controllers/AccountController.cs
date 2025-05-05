using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoGGStore.Models;
using System.Threading.Tasks;

namespace ProyectoGGStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Acción para mostrar el formulario de login
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Verificamos que los campos no estén vacíos manualmente
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ViewData["ErrorMessage"] = "Todos los campos son obligatorios.";
                return View(model);  // Devolvemos la vista con el error
            }

            // Verifica las credenciales del usuario
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // Redirige al Home después de iniciar sesión correctamente
                }
                else
                {
                    ViewData["ErrorMessage"] = "Contraseña incorrecta.";
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "El usuario no existe.";
            }

            // Si el login falla o la validación no es correcta, redirige de nuevo con los errores
            return View(model);
        }

        // Acción para cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Acción para mostrar el formulario de registro
        public IActionResult Register()
        {
            return View();
        }

        // Acción para procesar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Aquí puedes hacer login automático si lo deseas
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Si hubo un error al registrar al usuario, agregar los errores al ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Si el registro falla, devolver la vista con los errores
            return View(model);
        }
    }
}
