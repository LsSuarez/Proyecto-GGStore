<<<<<<< HEAD
using ProyectoGGStore.Models;
=======
using GGStore.Models;
using System.Collections.Generic;
>>>>>>> WebCompleta

namespace GGStore.Services
{
    public class ProductService
    {
        private List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Mouse Gamer", Description = "Mouse RGB", Price = 29.99M },
            new Product { Id = 2, Name = "Teclado Mecánico", Description = "Teclado RGB Switch Red", Price = 89.99M }
        };

        public List<Product> GetAllProducts() => _products;
    }
}
