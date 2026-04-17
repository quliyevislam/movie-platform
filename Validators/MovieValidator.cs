using MoviePlatform.Entities;
using MoviePlatform.Constants;
using FluentValidation;

namespace MoviePlatform.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(movie => movie.AccountId)
            .GreaterThan(0);

        RuleFor(movie => movie.Title)
            .NotNull()
            .Length(MovieConstants.TitleMinLength, MovieConstants.TitleMaxLength);

        RuleFor(movie => movie.Description)
            .MaximumLength(MovieConstants.DescriptionMaxLength);

        RuleFor(movie => movie.Genre)
            .MaximumLength(MovieConstants.GenreMaxLength);
    }
}