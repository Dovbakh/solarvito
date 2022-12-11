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
    /// Конфигурация таблицы Advertisements.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Advertisement>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.Advertisement> builder)
        {
            builder.ToTable("Advertisements");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Name).HasMaxLength(50);

            builder.Property(a => a.Description).HasMaxLength(2000);

            builder.Property(a => a.Address).HasMaxLength(2000);

            builder.Property(a => a.Phone).HasMaxLength(50);

            //builder.HasOne(a => a.Category)
            //    .WithMany(c => c.Advertisements)
            //    .HasForeignKey(a => a.CategoryId);

            //builder.HasOne(a => a.User)
            //.WithMany(u => u.Advertisements)
            //.HasForeignKey(a => a.UserId);

        }
    }
}
