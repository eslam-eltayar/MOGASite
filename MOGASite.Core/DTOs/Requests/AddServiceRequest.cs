using Microsoft.AspNetCore.Http;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Requests
{
    public class AddServiceRequest
    {
        public string TitleAR { get; set; } = string.Empty;
        public string TitleEN { get; set; } = string.Empty;

        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;

        public string BioAR { get; set; } = string.Empty;
        public string BioEN { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public IFormFile Image { get; set; }

        public List<ServiceStepsRequest> ServiceSteps { get; set; } = new List<ServiceStepsRequest>();
    }
}
