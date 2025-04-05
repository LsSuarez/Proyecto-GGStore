using System.ComponentModel.DataAnnotations;

namespace GGStoreProyecto.Models // Asegúrate de que el espacio de nombres sea correcto
{
    public class Product
    {
        public int Id { get; set; }

        // Constructor para asegurar que las propiedades no sean nulas
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            ImageUrl = string.Empty;
        }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string Name { get; set; }  // Nombre del producto

        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción del producto no puede exceder los 500 caracteres.")]
        public string Description { get; set; }  // Descripción del producto

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Price { get; set; }  // Precio del producto

        [Required(ErrorMessage = "La URL de la imagen es obligatoria.")]
        [Url(ErrorMessage = "La URL de la imagen no es válida.")]
        public string ImageUrl { get; set; }  // URL de la imagen
    }
}
