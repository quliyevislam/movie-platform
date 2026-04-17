using FluentValidation;
using MoviePlatform.Validators;

namespace MoviePlatform.Contracts.Movie;

public class MovieResponse
{
    public int Id { get; private set; }
    public int AccountId { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Genre { get; private set; }
    public DateOnly ReleaseDate { get; private set; }

    private MovieResponse() { }

    public class Builder
    {
        private readonly MovieResponse _movieResponse = new();
        private readonly MovieResponseValidator _movieResponseValidator = new();

        public Builder WithId(int id)
        {
            _movieResponse.Id = id;

            return this;
        }

        public Builder WithAccountId(int accountId)
        {
            _movieResponse.AccountId = accountId;

            return this;
        }

        public Builder WithTitle(string title)
        {
            _movieResponse.Title = title;

            return this;
        }

        public Builder WithDescription(string? description)
        {
            _movieResponse.Description = description;

            return this;
        }

        public Builder WithGenre(string? genre)
        {
            _movieResponse.Genre = genre;

            return this;
        }

        public Builder WithReleaseDate(DateOnly releaseDate)
        {
            _movieResponse.ReleaseDate = releaseDate;

            return this;
        }

        public MovieResponse Build()
        {
            _movieResponseValidator.ValidateAndThrow(_movieResponse);

            return _movieResponse;
        }
    }
}