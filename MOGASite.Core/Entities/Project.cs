using MOGASite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Entities
{
    public class Project : BaseEntity
    {
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;

        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;

        public string HeadImageUrl { get; set; } = string.Empty;

        public Category Category { get; set; }
        public ProjectType Type { get; set; }


        public ICollection<ProjectMedia> MediaItems { get; set; } = new HashSet<ProjectMedia>();
        public ICollection<ProjectSteps> ProjectSteps { get; set; } = new HashSet<ProjectSteps>();
        public ICollection<ProjectReview> ProjectReviews { get; set; } = new HashSet<ProjectReview>();
    }
}
