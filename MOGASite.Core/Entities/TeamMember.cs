using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Entities
{
    public class TeamMember : BaseEntity
    {
        public string FirstNameEN { get; set; } = string.Empty;
        public string FirstNameAR { get; set; } = string.Empty;

        public string LastNameEN { get; set; } = string.Empty;
        public string LastNameAR { get; set; } = string.Empty;

        public string PositionEN { get; set; } = string.Empty;
        public string PositionAR { get; set; } = string.Empty;
       
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
