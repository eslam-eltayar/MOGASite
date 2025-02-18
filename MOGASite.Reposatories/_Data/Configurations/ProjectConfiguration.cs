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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {

            builder.Property(p => p.NameAR)
                   .IsRequired()
                   .HasMaxLength(250);
 
            builder.Property(p => p.NameEN)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(p => p.DescriptionAR)
                   .IsRequired()
                   .HasMaxLength(2500);

             builder.Property(p => p.DescriptionEN)
                   .IsRequired()
                   .HasMaxLength(2500);

            builder.Property(p => p.Category)
                   .IsRequired();

            builder.Property(c => c.Category)
              .HasConversion(
                (type) => type.ToString(),
                (gen) => (Category)Enum.Parse(typeof(Category), gen, true));

            builder.Property(c => c.Type)
             .HasConversion((type) => type.ToString(),
                            (gen) => (ProjectType)Enum.Parse(typeof(ProjectType), gen, true));

        }
    }
}
