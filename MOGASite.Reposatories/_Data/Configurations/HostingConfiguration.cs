using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Reposatories._Data.Configurations
{
    public class HostingConfiguration : IEntityTypeConfiguration<Hosting>
    {
        public void Configure(EntityTypeBuilder<Hosting> builder)
        {

            builder.Property(x => x.NameAR).IsRequired().HasMaxLength(150);

            builder.Property(x => x.NameEN).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasMany(x => x.HostingProperties).WithOne(x => x.Hosting).HasForeignKey(x => x.HostingId);
        }
    }
}
