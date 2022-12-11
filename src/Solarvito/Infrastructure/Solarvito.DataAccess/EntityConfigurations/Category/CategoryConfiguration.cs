using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Solarvito.DataAccess.EntityConfigurations.Category
{
    /// <summary>
    /// Конфигурация таблицы Categories.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Category>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).HasMaxLength(100);

            builder.HasMany(с => с.Advertisements)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId);

        }
    }
}
