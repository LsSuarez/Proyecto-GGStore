using Microsoft.AspNetCore.Identity;

namespace GGStore.Models
{
    public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; } = "Default Full Name"; // Agregar ? para permitir nulos
}

}
