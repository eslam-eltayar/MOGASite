namespace MOGASite.Core.DTOs.Requests
{
    public class AddClientRequest
    {
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;

        public IFormFile Logo { get; set; }
    }
}
