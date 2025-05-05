using System.ComponentModel.DataAnnotations;
using ProyectoGGStore.Data;
using ProyectoGGStore.Models;

namespace ProyectoGGStore.Models
{
    public class Order
    {
        public int Id { get; set; }

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
    }
}
