using MoviePlatform.Constants;
using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Contracts.Movie;

public class MovieCreateRequest
{
    [Required]
    [Length(MovieConstants.TitleMinLength, MovieConstants.TitleMaxLength)]
    public string Title { get; set; } = null!;
    [MaxLength(MovieConstants.DescriptionMaxLength)]
    public string? Description { get; set; }
    [MaxLength(MovieConstants.GenreMaxLength)]
    public string? Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
}