using MoviePlatform.Validators;
using FluentValidation;

namespace MoviePlatform.Entities;

public class Movie
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }

    private Movie() { }

    public class Builder
    {
        private readonly Movie _movie = new();
        private readonly MovieValidator _movieValidator = new();

        public Builder WithAccountId(int accountId)
        {
            _movie.AccountId = accountId;

            return this;
        }

        public Builder WithTitle(string title)
        {
            _movie.Title = title;

            return this;
        }

        public Builder WithDescription(string? description)
        {
            _movie.Description = description;

            return this;
        }

        public Builder WithGenre(string? genre)
        {
            _movie.Genre = genre;

            return this;
        }

        public Builder WithReleaseDate(DateOnly releaseDate)
        {
            _movie.ReleaseDate = releaseDate;

            return this;
        }

        public Movie Build()
        {
            _movieValidator.ValidateAndThrow(_movie);

            return _movie;
        }
    }
}