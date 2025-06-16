using MOGASite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Entities
{
    public class Service : BaseEntity
    {
        public string TitleAR { get; set; } = string.Empty;
        public string TitleEN { get; set; } = string.Empty;

        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;

        public string BioAR { get; set; } = string.Empty;
        public string BioEN { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public ProjectType Type { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public bool HasPlan { get; set; } = false;
        public ICollection<ServiceSteps> ServiceSteps { get; set; } = new HashSet<ServiceSteps>();
    }
}
