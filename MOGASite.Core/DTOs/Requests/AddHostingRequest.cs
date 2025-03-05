namespace MOGASite.Core.DTOs.Requests
{
    public class AddHostingRequest
    {
        public string NameEN { get; set; } = string.Empty;
        public string NameAR { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0m;
        public bool IsBest { get; set; }

        public List<HostingPropertiesRequest> HostingProperties { get; set; } = new List<HostingPropertiesRequest>();
    }
}
