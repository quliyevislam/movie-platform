using MoviePlatform.Contracts.Auth;
using FluentValidation;

namespace MoviePlatform.Validators;

public class AuthResponseValidator : AbstractValidator<AuthResponse>
{
    public AuthResponseValidator()
    {
        RuleFor(accountResponse => accountResponse.Token)
            .NotNull();

        RuleFor(accountResponse => accountResponse.User)
            .NotNull();
    }
}