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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            builder.Property(x => x.FirstNameAR).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FirstNameEN).IsRequired().HasMaxLength(100);


            builder.Property(x => x.LastNameAR).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastNameEN).IsRequired().HasMaxLength(100);

            builder.Property(x => x.PositionAR).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PositionEN).IsRequired().HasMaxLength(200);

            builder.Property(x => x.ReviewTextAR).IsRequired().HasMaxLength(2500);
            builder.Property(x => x.ReviewTextEN).IsRequired().HasMaxLength(2500);

        }
    }
}
