using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Requests
{
    public class AddTeamRequest
    {
        public string FirstNameEN { get; set; } = string.Empty;
        public string FirstNameAR { get; set; } = string.Empty;

        public string LastNameEN { get; set; } = string.Empty;
        public string LastNameAR { get; set; } = string.Empty;

        public string PositionEN { get; set; } = string.Empty;
        public string PositionAR { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

    }
}
