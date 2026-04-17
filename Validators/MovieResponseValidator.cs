using MoviePlatform.Contracts.Movie;
using MoviePlatform.Constants;
using FluentValidation;

namespace MoviePlatform.Validators;

public class MovieResponseValidator : AbstractValidator<MovieResponse>
{
    public MovieResponseValidator()
    {
        RuleFor(movieResponse => movieResponse.Id)
            .GreaterThan(0);

        RuleFor(movieResponse => movieResponse.AccountId)
            .GreaterThan(0);

        RuleFor(movieResponse => movieResponse.Title)
            .NotNull()
            .Length(MovieConstants.TitleMinLength, MovieConstants.TitleMaxLength);

        RuleFor(movieResponse => movieResponse.Description)
            .MaximumLength(MovieConstants.DescriptionMaxLength);

        RuleFor(movieResponse => movieResponse.Genre)
            .MaximumLength(MovieConstants.GenreMaxLength);
    }
}