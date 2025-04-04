using GGStoreProyecto.Data; // Asegúrate de que esta referencia esté aquí
using Microsoft.AspNetCore.Authentication.Cookies; // Asegúrate de importar esto
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GGStoreProyecto.Models;  // Asegúrate de que has importado tu modelo ApplicationUser

var builder = WebApplication.CreateBuilder(args);

// Configuración del DbContext para usar SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Identity (deberías tener ApplicationUser configurado)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuración de autenticación con Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";  // Ruta de inicio de sesión
        options.LogoutPath = "/Usuario/Logout";  // Ruta de cierre de sesión
        options.AccessDeniedPath = "/Home/AccessDenied";  // Ruta de acceso denegado
    });

// Configuración de autorización
builder.Services.AddAuthorization(); // Asegúrate de habilitar la autorización

// Agregar controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración de las rutas y middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Habilitar página de excepción en desarrollo
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Manejo de errores en producción
    app.UseHsts();  // Usar HSTS para HTTP seguro
}

// Habilitar redirección a HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();  // Servir archivos estáticos (CSS, JS, imágenes, etc.)
app.UseRouting();  // Habilitar el enrutamiento

// Necesitamos asegurarnos de que la autenticación esté habilitada antes de las rutas
app.UseAuthentication();  // Habilita la autenticación
app.UseAuthorization();   // Habilita la autorización

// Configuración de las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Iniciar la aplicación
app.Run();
