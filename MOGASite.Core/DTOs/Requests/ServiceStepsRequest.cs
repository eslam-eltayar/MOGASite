using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Requests
{
    public class ServiceStepsRequest
    {
        public string TitleAR { get; set; } = string.Empty;
        public string TitleEN { get; set; } = string.Empty;
        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;
        public string BioAR { get; set; } = string.Empty;
        public string BioEN { get; set; } = string.Empty;

        public IFormFile Image { get; set; } 

    }
}
