// En Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using GGStoreProyecto.Models;  // Asegúrate de que el espacio de nombres Models esté correcto


namespace GGStoreProyecto.Data // Asegúrate de que este espacio de nombres coincida
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }  // Referencia al modelo Product
    }
}
