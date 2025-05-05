using System;
using System.Collections.Generic;

namespace GGStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public DateTime OrderDate { get; set; }

        // Relación de uno a muchos con Product
        public ICollection<Product> Products { get; set; }
    }
}
