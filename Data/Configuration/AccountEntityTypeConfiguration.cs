using MoviePlatform.Entities;
using MoviePlatform.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviePlatform.Data.Configuration;

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("account");

        entityTypeBuilder.ToTable(
            table => table.HasCheckConstraint(
                "CK_account_name_min_length",
                $"length(name) >= {AccountConstants.NameMinLength}"
            )
        );

        entityTypeBuilder.HasIndex(account => account.Email).IsUnique();

        entityTypeBuilder
            .Property(account => account.Id)
            .HasColumnName("id");

        entityTypeBuilder
            .Property(account => account.Name)
            .HasMaxLength(AccountConstants.NameMaxLength)
            .HasColumnName("name");

        entityTypeBuilder
            .Property(account => account.Email)
            .HasMaxLength(AccountConstants.EmailMaxLength)
            .HasColumnName("email");

        entityTypeBuilder
            .Property(account => account.PasswordHash)
            .HasColumnName("password_hash");
    }
}