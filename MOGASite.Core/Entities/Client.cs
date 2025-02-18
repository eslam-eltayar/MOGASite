using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Entities
{
    public class Client : BaseEntity
    {
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;

        public string LogoUrl { get; set; } = string.Empty;
    }
}
