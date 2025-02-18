namespace MOGASite.Core.Entities
{
    public class ProjectSteps : BaseEntity
    {
        public string TitleEN { get; set; } = string.Empty;
        public string TitleAR { get; set; } = string.Empty;

        public string DescriptionEN { get; set; } = string.Empty;
        public string DescriptionAR { get; set; } = string.Empty;

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}