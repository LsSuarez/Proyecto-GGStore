using System.ComponentModel.DataAnnotations;

<<<<<<< HEAD
namespace ProyectoGGStore.Models
=======
namespace GGStore.Models
>>>>>>> WebCompleta
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
<<<<<<< HEAD
        [StringLength(100)]
        public string Name { get; set; }

        [Required]  // Asegura que Description no sea nulo
        [StringLength(250)]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        // Esta es la propiedad que almacena la ruta de la imagen
        public string ImageUrl { get; set; }

        // Constructor sin parámetros
        public Product() { }

        // Constructor con 5 parámetros (incluyendo ImageUrl)
        public Product(int id, string name, string description, decimal price, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;  // Asigna la ruta de la imagen
        }
=======
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
>>>>>>> WebCompleta
    }
}
