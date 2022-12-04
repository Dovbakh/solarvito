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

            builder.Property(u => u.Name).HasMaxLength(50);

            builder.Property(u => u.Login).HasMaxLength(50);

            //builder.Property(u => u.Adress).

            builder.HasMany(u => u.Advertisements)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);


        }
    }
}
