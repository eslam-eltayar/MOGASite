using Microsoft.AspNetCore.Http;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Requests
{
    public class UpdateBlogRequest
    {
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public string ContentAR { get; set; }
        public string ContentEN { get; set; }

        public string Category { get; set; }

        public IFormFile? Image { get; set; }

        //public List<BlogContentResponse> BlogContents { get; set; } = new List<BlogContentResponse>();
    }
}
