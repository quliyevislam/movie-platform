using MoviePlatform.Validators;
using FluentValidation;

namespace MoviePlatform.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    private Account() { }

    public class Builder
    {
        private readonly Account _account = new();
        private readonly AccountValidator _accountValidator = new();

        public Builder WithName(string name)
        {
            _account.Name = name;

            return this;
        }

        public Builder WithEmail(string email)
        {
            _account.Email = email;

            return this;
        }

        public Builder WithPasswordHash(string passwordHash)
        {
            _account.PasswordHash = passwordHash;

            return this;
        }

        public Account Build()
        {
            _accountValidator.ValidateAndThrow(_account);

            return _account;
        }
    }
}