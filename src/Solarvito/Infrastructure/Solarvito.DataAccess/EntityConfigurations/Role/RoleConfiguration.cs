using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Role
{
    /// <summary>
    /// Конфигурация таблицы Roles.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Domain.Role>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
