using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Responses
{
    public class HostingResponse
    {
        public int Id { get; set; }
        public string NameEN { get; set; } = string.Empty;
        public string NameAR { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0m;
        public bool IsBest { get; set; }

        public List<HostingPropertiesRequest> Hosting_Properties { get; set; } = new List<HostingPropertiesRequest>();
    }
}
