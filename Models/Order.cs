using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GGStoreProyecto.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public required string UserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public required string PaymentMethod { get; set; }

        [Required]
        public required virtual ICollection<OrderDetail> OrderDetails { get; set; }

        // Agregar la propiedad TotalPrice
        [Range(0, double.MaxValue, ErrorMessage = "El precio total debe ser un valor positivo.")]
        public decimal TotalPrice { get; set; }
    }
}
