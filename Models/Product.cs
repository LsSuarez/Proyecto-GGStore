using System.ComponentModel.DataAnnotations;

namespace GGStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;  // Inicializa con un valor predeterminado vacío

        [Required]
        public string Description { get; set; } = string.Empty;  // Inicializa con un valor predeterminado vacío

        [Required]
        public decimal Price { get; set; } = 0;  // Inicializa con un valor predeterminado de 0
    }
}
