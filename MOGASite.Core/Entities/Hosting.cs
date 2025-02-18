using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Entities
{
    public class Hosting : BaseEntity
    {
        public string NameEN { get; set; } = string.Empty;
        public string NameAR { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0m;
        public bool IsBest { get; set; } = false;

        public ICollection<HostingProperties> HostingProperties { get; set; } = new HashSet<HostingProperties>();
    }
}
