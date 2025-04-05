using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GGStoreProyecto.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // ⚠️ IMPORTANTE: inicializado

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser al menos 1.")]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; } // ← Hacelo opcional con '?'
    }
}
