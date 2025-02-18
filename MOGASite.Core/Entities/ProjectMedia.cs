namespace MOGASite.Core.Entities
{
    public class ProjectMedia : BaseEntity
    {
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public string MediaUrl { get; set; } = string.Empty;
    }
}