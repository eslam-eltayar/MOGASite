using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Responses
{
    public class QuotationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BusinessEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; }

        public string Notes { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

    }
}
