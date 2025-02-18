using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Responses
{
    public class ProjectResponse
    {
        public int ProjectId { get; set; }
        public string TitleEN { get; set; } = string.Empty;
        public string TitleAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;
        public string DescriptionAR { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string HeadImageUrl { get; set; } = string.Empty;

        public List<ProjectStepsResponse> ProjectSteps { get; set; } = new List<ProjectStepsResponse>();
        public List<string> MediaUrls { get; set; } = new List<string>();

    }
}
