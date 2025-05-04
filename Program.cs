using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoGGStore.Data;
using ProyectoGGStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar los servicios de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar Identity para la gestión de usuarios
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración para servir archivos estáticos (como imágenes, CSS, JS)
app.UseStaticFiles();  // Esto es necesario para que puedas acceder a archivos en wwwroot

// Crear los roles de "Admin" y "User" si no existen
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" }; // Define los roles
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var role = new IdentityRole(roleName);
            await roleManager.CreateAsync(role);
        }
    }

    // Crear un administrador predeterminado (solo para pruebas o producción inicial)
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            FullName = "Administrador"  // Asignamos el FullName aquí correctamente
        };

        var result = await userManager.CreateAsync(adminUser, "Password123!"); // Cambia la contraseña por algo seguro
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// Configuración de enrutamiento para las vistas
app.UseRouting();

// Configuración de middleware para usar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Definir ruta predeterminada para las vistas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicación
app.Run();
