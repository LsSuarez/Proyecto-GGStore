using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GGStore.Data;
using GGStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configurar Identity
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

    // Crear rol Admin si no existe
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var role = new IdentityRole("Admin");
        await roleManager.CreateAsync(role);
    }

    // Crear rol User si no existe
    if (!await roleManager.RoleExistsAsync("User"))
    {
        var role = new IdentityRole("User");
        await roleManager.CreateAsync(role);
    }

    // Crear un usuario Admin por defecto (si no existe) y asignarle el rol "Admin"
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            FullName = "Administrator" // Establecer un valor para FullName
        };
        var result = await userManager.CreateAsync(adminUser, "Password123!"); // Cambiar la contraseña por una segura
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Crear un usuario User por defecto (si no existe) y asignarle el rol "User"
    var regularUser = await userManager.FindByEmailAsync("user@example.com");
    if (regularUser == null)
    {
        regularUser = new ApplicationUser
        {
            UserName = "user",
            Email = "user@example.com",
            FullName = "Regular User" // Establecer un valor para FullName
        };
        var result = await userManager.CreateAsync(regularUser, "Password123!"); // Cambiar la contraseña por una segura
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(regularUser, "User");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Agregar autenticación
app.UseAuthorization();   // Agregar autorización para los roles

// Definir las rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Iniciar la aplicación
app.Run();
