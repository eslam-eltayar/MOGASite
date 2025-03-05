namespace MOGASite.Core.DTOs.Requests
{
    public class AddQuotationRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BusinessEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; }

        public string Notes { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;

    }
}
