using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using GGStoreProyecto.Models; // Asegúrate de que se ha referenciado el modelo LoginViewModel

namespace GGStoreProyecto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor
        public UsuarioController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Mostrar vista de login
        public IActionResult Login()
        {
            return View(); // Aquí va tu vista de Login
        }

        // Lógica de inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken] // Protege contra ataques CSRF
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Intentar autenticar al usuario con el UserManager (en vez de hardcodear "admin" y "password")
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    // Verifica si la contraseña es correcta
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    
                    if (result.Succeeded)
                    {
                        // Crea los Claims
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        // Autenticar al usuario
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        // Redirigir a la página anterior (si la existe) o a la página principal
                        var returnUrl = HttpContext.Request.Query["ReturnUrl"];
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Si las credenciales son incorrectas
                        ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                    }
                }
                else
                {
                    // Si no se encuentra el usuario
                    ModelState.AddModelError(string.Empty, "Usuario no encontrado.");
                }
            }

            // Si llega aquí, es porque la validación falló o las credenciales no fueron correctas
            return View(model);
        }

        // Cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");  // Redirige a la página principal después de cerrar sesión
        }
    }
}
