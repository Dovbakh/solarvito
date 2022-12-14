using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.AdvertisementImage
{
    /// <summary>
    /// Конфигурация таблицы AdvertisementImages
    /// </summary>
    public class AdvertisementImageConfiguration : IEntityTypeConfiguration<Domain.AdvertisementImage>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.AdvertisementImage> builder)
        {
            builder.ToTable("AdvertisementImages");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.FileName).HasMaxLength(100);

        }
    }
}
