using MoviePlatform.Validators;
using MoviePlatform.Contracts.Account;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MoviePlatform.Contracts.Auth;

public class AuthResponse
{
    public string Token { get; private set; } = null!;
    public DateTime Expires { get; private set; }
    public AccountResponse User { get; private set; } = null!;

    private AuthResponse() { }

    public class Builder
    {
        private readonly AuthResponse _authResponse = new();
        private readonly AuthResponseValidator _authResponseValidator = new();

        public Builder WithToken(string token)
        {
            _authResponse.Token = token;

            return this;
        }

        public Builder WithExpires(DateTime expires)
        {
            _authResponse.Expires = expires;

            return this;
        }

        public Builder WithUser(AccountResponse user)
        {
            _authResponse.User = user;

            return this;
        }

        public AuthResponse Build()
        {
            _authResponseValidator.ValidateAndThrow(_authResponse);

            return _authResponse;
        }
    }
}