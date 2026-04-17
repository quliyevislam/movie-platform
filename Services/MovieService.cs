using MoviePlatform.Data;
using MoviePlatform.Contracts.Movie;
using MoviePlatform.Exceptions;
using MoviePlatform.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MoviePlatform.Services;

public class MovieService
{
    private readonly MoviePlatformDbContext _moviePlatformDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MovieService(
        MoviePlatformDbContext moviePlatformDbContext,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _moviePlatformDbContext = moviePlatformDbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<MovieResponse> CreateMovieForUser(MovieCreateRequest movieCreateRequest)
    {
        var movie = new Movie.Builder()
            .WithAccountId(GetUserId())
            .WithTitle(movieCreateRequest.Title)
            .WithDescription(movieCreateRequest.Description)
            .WithGenre(movieCreateRequest.Genre)
            .WithReleaseDate(movieCreateRequest.ReleaseDate)
            .Build();

        _moviePlatformDbContext.Movies.Add(movie);
        await _moviePlatformDbContext.SaveChangesAsync();

        return MapToMovieResponse(movie);
    }

    public async Task<MovieResponse> UpdateMovieForUser(int id, MovieUpdateRequest movieUpdateRequest)
    {
        var movie = await _moviePlatformDbContext.Movies.FindAsync(id)
            ?? throw new NotFoundException("Movie not found");

        if (movie.AccountId != GetUserId())
        {
            throw new ForbiddenException("Unauthorized access to resource");
        }

        movie.Title = movieUpdateRequest.Title;
        movie.Description = movieUpdateRequest.Description;
        movie.Genre = movieUpdateRequest.Genre;
        movie.ReleaseDate = movieUpdateRequest.ReleaseDate;

        await _moviePlatformDbContext.SaveChangesAsync();

        return MapToMovieResponse(movie);
    }

    public async Task<IEnumerable<MovieResponse>> GetAllMoviesForUser()
    {
        var movies = await _moviePlatformDbContext.Movies
            .Where(m => m.AccountId == GetUserId())
            .ToListAsync();

        return movies.Select(movie => MapToMovieResponse(movie));
    }

    public async Task<MovieResponse> GetMovieByIdForUser(int id)
    {
        var movie = await _moviePlatformDbContext.Movies.FindAsync(id)
            ?? throw new NotFoundException("Movie not found");

        if (movie.AccountId != GetUserId())
        {
            throw new ForbiddenException("Unauthorized access to resource");
        }

        return MapToMovieResponse(movie);
    }

    public async Task<bool> DeleteMovieByIdForUser(int id)
    {
        var movie = await _moviePlatformDbContext.Movies.FindAsync(id);

        if (movie is null)
        {
            return false;
        }

        if (movie.AccountId != GetUserId())
        {
            throw new ForbiddenException("Unauthorized access to resource");
        }

        _moviePlatformDbContext.Movies.Remove(movie);
        await _moviePlatformDbContext.SaveChangesAsync();

        return true;
    }

    private int GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User
            ?? throw new UserNotAuthenticatedException("User is not logged in");

        return int.Parse(
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidUserIdentifierException("User id not found")
        );
    }

    private MovieResponse MapToMovieResponse(Movie movie)
    {
        return new MovieResponse.Builder()
            .WithId(movie.Id)
            .WithAccountId(movie.AccountId)
            .WithTitle(movie.Title)
            .WithDescription(movie.Description)
            .WithGenre(movie.Genre)
            .WithReleaseDate(movie.ReleaseDate)
            .Build();
    }
}