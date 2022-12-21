using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Role
{
    /// <summary>
    /// Конфигурация таблицы AspNetRoles.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Domain.Role>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.Role> builder)
        {
            builder.ToTable("AspNetRoles");

            builder.Property(r => r.Name).HasMaxLength(100);

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

        }
    }
}
