using MoviePlatform.Constants;
using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Contracts.Auth;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(AccountConstants.EmailMaxLength)]
    public string Email { get; set; } = null!;
    [Required]
    [MinLength(AccountConstants.PasswordMinLength)]
    public string Password { get; set; } = null!;
}