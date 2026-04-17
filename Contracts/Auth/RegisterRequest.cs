using MoviePlatform.Constants;
using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Contracts.Auth;

public class RegisterRequest
{
    [Required]
    [Length(AccountConstants.NameMinLength, AccountConstants.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [EmailAddress]
    [MaxLength(AccountConstants.EmailMaxLength)]
    public string Email { get; set; } = null!;
    [Required]
    [MinLength(AccountConstants.PasswordMinLength)]
    public string Password { get; set; } = null!;
}