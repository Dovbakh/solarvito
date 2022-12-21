using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Solarvito.DataAccess.EntityConfigurations.User
{
    /// <summary>
    /// Конфигурация таблицы AspNetUsers.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<Domain.User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.User> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.Property(u => u.Name).HasMaxLength(100);
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
