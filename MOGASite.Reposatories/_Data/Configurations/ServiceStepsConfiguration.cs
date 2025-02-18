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
    public class ServiceStepsConfiguration : IEntityTypeConfiguration<ServiceSteps>
    {
        public void Configure(EntityTypeBuilder<ServiceSteps> builder)
        {

            builder.Property(ss => ss.TitleAR).IsRequired().HasMaxLength(255);
            builder.Property(ss => ss.TitleEN).IsRequired().HasMaxLength(255);

            builder.Property(ss => ss.DescriptionAR).IsRequired().HasMaxLength(1000);
            builder.Property(ss => ss.DescriptionEN).IsRequired().HasMaxLength(1000);

            builder.Property(ss => ss.BioAR).HasMaxLength(1000);
            builder.Property(ss => ss.BioEN).HasMaxLength(1000);
        }
    }
}
