using MoviePlatform.Data;
using MoviePlatform.Entities;
using MoviePlatform.Contracts.Auth;
using MoviePlatform.Contracts.Account;
using MoviePlatform.Exceptions;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace MoviePlatform.Services;

public class AuthService
{
    private readonly MoviePlatformDbContext _moviePlatformDbContext;
    private readonly JwtService _jwtService;

    public AuthService(MoviePlatformDbContext moviePlatformDbContext, JwtService jwtService)
    {
        _moviePlatformDbContext = moviePlatformDbContext;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> CreateAccount(RegisterRequest registerRequest)
    {
        var exists = await _moviePlatformDbContext.Accounts.AnyAsync(
            account => account.Email.Equals(registerRequest.Email)
        );

        if (exists)
        {
            throw new DuplicateEmailException("Account with this email already exists");
        }

        var account = new Account.Builder()
            .WithName(registerRequest.Name)
            .WithEmail(registerRequest.Email)
            .WithPasswordHash(BC.HashPassword(registerRequest.Password))
            .Build();

        _moviePlatformDbContext.Accounts.Add(account);
        await _moviePlatformDbContext.SaveChangesAsync();

        return MapToAuthResponse(account);
    }

    public async Task<AuthResponse> Authenticate(LoginRequest loginRequest)
    {
        var exists = await _moviePlatformDbContext.Accounts.AnyAsync(
            account => account.Email.Equals(loginRequest.Email)
        );

        if (!exists)
        {
            throw new InvalidCredentialsException();
        }

        var account = await _moviePlatformDbContext.Accounts
            .SingleAsync(account => account.Email.Equals(loginRequest.Email));

        if (!BC.Verify(loginRequest.Password, account.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        return MapToAuthResponse(account);
    }

    private AuthResponse MapToAuthResponse(Account account)
    {
        var generateTokenResult = _jwtService.GenerateToken(account);

        return new AuthResponse.Builder()
            .WithToken(generateTokenResult.Token)
            .WithExpires(generateTokenResult.Expires)
            .WithUser(
                new AccountResponse.Builder()
                    .WithId(account.Id)
                    .WithName(account.Name)
                    .WithEmail(account.Email)
                    .Build()
            )
            .Build();
    }
}