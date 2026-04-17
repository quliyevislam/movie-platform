using FluentValidation;
using MoviePlatform.Validators;

namespace MoviePlatform.Contracts.Account;

public class AccountResponse
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    private AccountResponse() { }

    public class Builder
    {
        private readonly AccountResponse _accountResponse = new();
        private readonly AccountResponseValidator _accountResponseValidator = new();

        public Builder WithId(int id)
        {
            _accountResponse.Id = id;

            return this;
        }

        public Builder WithName(string name)
        {
            _accountResponse.Name = name;

            return this;
        }

        public Builder WithEmail(string email)
        {
            _accountResponse.Email = email;

            return this;
        }

        public AccountResponse Build()
        {
            _accountResponseValidator.ValidateAndThrow(_accountResponse);

            return _accountResponse;
        }
    }
}