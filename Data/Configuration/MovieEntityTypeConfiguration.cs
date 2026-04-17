using MoviePlatform.Entities;
using MoviePlatform.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviePlatform.Data.Configuration;

public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("movie");

        entityTypeBuilder.ToTable(
            table => table.HasCheckConstraint(
                "CK_movie_title_min_length",
                $"length(title) >= {MovieConstants.TitleMinLength}"
            )
        );

        entityTypeBuilder
            .Property(movie => movie.Id)
            .HasColumnName("id");

        entityTypeBuilder
            .Property(movie => movie.AccountId)
            .HasColumnName("account_id");

        entityTypeBuilder
            .Property(movie => movie.Title)
            .HasMaxLength(MovieConstants.TitleMaxLength)
            .HasColumnName("title");

        entityTypeBuilder
            .Property(movie => movie.Description)
            .HasMaxLength(MovieConstants.DescriptionMaxLength)
            .HasColumnName("description");

        entityTypeBuilder
            .Property(movie => movie.Genre)
            .HasMaxLength(MovieConstants.GenreMaxLength)
            .HasColumnName("genre");

        entityTypeBuilder
            .Property(movie => movie.ReleaseDate)
            .HasColumnName("release_date");
    }
}