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
}
