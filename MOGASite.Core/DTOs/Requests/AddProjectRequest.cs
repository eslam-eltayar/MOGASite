namespace MOGASite.Core.DTOs.Requests
{
    public class AddProjectRequest
    {
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;

        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public IFormFile HeadImage { get; set; }

        public List<IFormFile> MediaFiles { get; set; } = new List<IFormFile>();
        public List<ProjectStepsRequest> ProjectSteps { get; set; } = new List<ProjectStepsRequest>();

    }
}
