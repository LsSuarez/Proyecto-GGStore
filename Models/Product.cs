using System.ComponentModel.DataAnnotations;

namespace ProyectoGGStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]  // Asegura que Description no sea nulo
        [StringLength(250)]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        public Product() { }

        public string ImageUrl { get; set; }

        public Product(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;  // Asigna el valor a Description
            Price = price;
        }
    }
}
