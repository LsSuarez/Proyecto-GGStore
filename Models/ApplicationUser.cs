using Microsoft.AspNetCore.Identity;

namespace ProyectoGGStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Propiedad adicional
        public string FullName { get; set; }

        // Constructor por defecto
        public ApplicationUser() : base() { }

        // Constructor con parámetros
        public ApplicationUser(string fullName)
        {
            FullName = fullName;
        }
    }
}
