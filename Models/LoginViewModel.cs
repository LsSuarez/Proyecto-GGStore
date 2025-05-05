using System.ComponentModel.DataAnnotations;  // Asegúrate de tener esta referencia

namespace GGStore.ViewModels
{
    public class LoginViewModel
    {
        // Propiedad para el nombre de usuario
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; } = string.Empty;  // Inicializamos con un valor vacío

        // Propiedad para la contraseña
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]  // Campo de tipo contraseña
        public string Password { get; set; } = string.Empty;  // Inicializamos con un valor vacío

        // Propiedad para la opción de "Recordarme"
        public bool RememberMe { get; set; }  // Propiedad que controla la opción de "Recordarme"
    }
}
