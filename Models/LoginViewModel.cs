using System.ComponentModel.DataAnnotations;

namespace GGStoreProyecto.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de usuario debe tener un máximo de 100 caracteres.")]
        public string Username { get; set; } = string.Empty; // Valor predeterminado

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La contraseña debe tener un máximo de 100 caracteres.")]
        public string Password { get; set; } = string.Empty; // Valor predeterminado

        // Constructor vacío
        public LoginViewModel() { }

        // Constructor con parámetros
        public LoginViewModel(string username = "", string password = "")
        {
            Username = username ?? string.Empty;  // Asegura que no sea null
            Password = password ?? string.Empty;  // Asegura que no sea null
        }
    }
}
