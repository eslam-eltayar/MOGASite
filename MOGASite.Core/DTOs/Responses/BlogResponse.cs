using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Responses
{
    public class BlogResponse
    {
        public int BlogId { get; set; }
        public string TitleAR { get; set; } = string.Empty;
        public string TitleEN { get; set; } = string.Empty;
        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public string ContentAR { get; set; }
        public string ContentEN { get; set; }

        public string Date { get; set; } = string.Empty;

        public string Slug { get; set; }

        //public List<BlogContentResponse> BlogContents { get; set; } = new List<BlogContentResponse>();
    }
}
