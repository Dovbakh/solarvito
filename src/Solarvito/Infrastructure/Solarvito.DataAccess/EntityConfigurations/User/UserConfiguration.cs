using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Advertisement
{
    /// <summary>
    /// Конфигурация таблицы Users.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<Domain.User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Name).HasMaxLength(100);
            builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
            builder.Property(u => u.Address).HasMaxLength(500);

            builder.HasMany(u => u.Advertisements)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.CommentsBy)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            builder.HasMany(u => u.CommentsFor)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}
