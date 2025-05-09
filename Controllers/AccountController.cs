using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GGStore.Models;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Vista de Login
    public IActionResult Login()
    {
        return View();
    }

    // Acción POST para iniciar sesión
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar al usuario por nombre de usuario
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                // Intentar iniciar sesión con el usuario
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false); // No usar RememberMe

                if (result.Succeeded)
                {
                    // Redirigir a la página principal (Home) después de un login exitoso
                    return RedirectToAction("Index", "Home"); // Redirigir al controlador Home
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }
        }

        // Si el modelo no es válido o el inicio de sesión falla
        return View(model);
    }

    // Vista de Registro
    public IActionResult Register()
    {
        return View();
    }

    // Acción POST para registrar un nuevo usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Asignar rol por defecto
                await _userManager.AddToRoleAsync(user, "User");

                // Iniciar sesión después del registro
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");  // Redirigir a la página de inicio
            }

            // Si la creación del usuario falla, agregar los errores al modelo
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
}
