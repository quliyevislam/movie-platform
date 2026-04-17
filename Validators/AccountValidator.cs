using MoviePlatform.Entities;
using MoviePlatform.Constants;
using FluentValidation;

namespace MoviePlatform.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(account => account.Name)
            .NotNull()
            .Length(AccountConstants.NameMinLength, AccountConstants.NameMaxLength);

        RuleFor(account => account.Email)
            .NotNull()
            .EmailAddress()
            .MaximumLength(AccountConstants.EmailMaxLength);

        RuleFor(account => account.PasswordHash)
            .NotNull();
    }
}