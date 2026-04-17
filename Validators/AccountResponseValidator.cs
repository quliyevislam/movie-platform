using MoviePlatform.Contracts.Account;
using MoviePlatform.Constants;
using FluentValidation;

namespace MoviePlatform.Validators;

public class AccountResponseValidator : AbstractValidator<AccountResponse>
{
    public AccountResponseValidator()
    {
        RuleFor(accountResponse => accountResponse.Id)
            .GreaterThan(0);

        RuleFor(accountResponse => accountResponse.Name)
            .NotNull()
            .Length(AccountConstants.NameMinLength, AccountConstants.NameMaxLength);

        RuleFor(accountResponse => accountResponse.Email)
            .NotNull()
            .EmailAddress()
            .MaximumLength(AccountConstants.EmailMaxLength);
    }
}