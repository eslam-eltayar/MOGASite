﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Requests
{
    public class ProjectStepsRequest
    {
        public string TitleEN { get; set; } = string.Empty;
        public string TitleAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;
        public string DescriptionAR { get; set; } = string.Empty;
    }
}
