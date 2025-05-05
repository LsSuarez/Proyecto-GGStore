using System.ComponentModel.DataAnnotations;

namespace GGStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Asegurando que Name no sea nulo, agregamos el modificador required
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;  // Inicialización con un valor predeterminado

        // Asegurando que Description no sea nulo
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;  // Inicialización con un valor predeterminado

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria.")]
        public string ImageUrl { get; set; } = string.Empty;  // Inicializamos con un valor predeterminado

        // Constructor sin parámetros
        public Product() { }

        // Constructor con parámetros, incluyendo ImageUrl
        public Product(int id, string name, string description, decimal price, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;  // Asigna la ruta de la imagen
        }
    }
}
