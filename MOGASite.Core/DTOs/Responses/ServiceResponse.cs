using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Responses
{
    public class ServiceResponse
    {
        public int Id { get; set; }

        public string TitleAR { get; set; } = string.Empty;
        public string TitleEN { get; set; } = string.Empty;

        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;

        public string BioAR { get; set; } = string.Empty;
        public string BioEN { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public List<ServiceStepsResponse> ServiceSteps { get; set; } = new List<ServiceStepsResponse>();
    }
}
