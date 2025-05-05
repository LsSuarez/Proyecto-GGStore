using System.ComponentModel.DataAnnotations;

namespace GGStore.ViewModels
{
    public class LoginViewModel
    {
        // Propiedad para el nombre de usuario
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; } = string.Empty;

        // Propiedad para la contraseña
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // Propiedad para la opción de "Recordarme"
        public bool RememberMe { get; set; }

        // Propiedad para el correo electrónico
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
        public string Email { get; set; } = string.Empty;  // Inicializamos con un valor vacío
    }
}
