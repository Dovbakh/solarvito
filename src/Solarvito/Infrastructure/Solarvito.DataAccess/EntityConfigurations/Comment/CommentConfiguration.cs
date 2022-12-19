using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess.EntityConfigurations.Comment
{
    public class CommentConfiguration : IEntityTypeConfiguration<Domain.Comment>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Domain.Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Text).HasMaxLength(1000);

        }
    }
}
