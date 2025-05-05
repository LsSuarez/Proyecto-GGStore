<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using ProyectoGGStore.Data;
using ProyectoGGStore.Models;

namespace ProyectoGGStore.Models
=======
using System;
using System.Collections.Generic;

namespace GGStore.Models
>>>>>>> WebCompleta
{
    public class Order
    {
        public int Id { get; set; }
<<<<<<< HEAD

        [Required]  // Asegurando que OrderDetails no sea nulo
        [StringLength(250)]
        public string OrderDetails { get; set; }

        public DateTime OrderDate { get; set; }

        public Order() { }

        public Order(int id, string orderDetails, DateTime orderDate)
        {
            Id = id;
            OrderDetails = orderDetails;
            OrderDate = orderDate;
        }
=======
        
        public DateTime OrderDate { get; set; }

        // Relación de uno a muchos con Product
        public ICollection<Product> Products { get; set; }
>>>>>>> WebCompleta
    }
}
