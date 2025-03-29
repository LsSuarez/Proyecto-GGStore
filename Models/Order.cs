using System.Collections.Generic;
using System.Linq;

namespace GGStoreProyecto.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public decimal TotalPrice => Products.Sum(p => p.Price);
    }
}