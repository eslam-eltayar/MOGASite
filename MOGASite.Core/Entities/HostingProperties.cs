namespace MOGASite.Core.Entities
{
    public class HostingProperties : BaseEntity
    {
        public string TitleEN { get; set; } = string.Empty;
        public string TitleAR { get; set; } = string.Empty;
        
        public Hosting Hosting { get; set; }
        public int HostingId { get; set; }
    }
}