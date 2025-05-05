<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace ProyectoGGStore.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
=======
public class RegisterViewModel
{
    
    public string UserName { get; set; } = string.Empty;  // Asegurarse de que no sea nulo

   
    public string Email { get; set; } = string.Empty;

   
    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}

public class LoginViewModel
{
    
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
>>>>>>> WebCompleta
}
