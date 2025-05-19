using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    [MinLength(4, ErrorMessage = "Username must be at least 4 characters.")]
    public string Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain upper, lower, and digit."
    )]
    public string Password { get; set; }
}
