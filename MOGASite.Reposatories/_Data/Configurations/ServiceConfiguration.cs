using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOGASite.Core.Entities;
using MOGASite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Reposatories._Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(s => s.TitleAR).IsRequired().HasMaxLength(255);
            builder.Property(s => s.TitleEN).IsRequired().HasMaxLength(255);

            builder.Property(s => s.DescriptionAR).IsRequired().HasMaxLength(1000);
            builder.Property(s => s.DescriptionEN).IsRequired().HasMaxLength(1000);

            builder.Property(s => s.BioAR).HasMaxLength(1000);
            builder.Property(s => s.BioEN).HasMaxLength(1000);

            builder.Property(s => s.Type)
             .HasConversion((type) => type.ToString(),
                            (gen) => (ProjectType)Enum.Parse(typeof(ProjectType), gen, true));
        }
    }
}
