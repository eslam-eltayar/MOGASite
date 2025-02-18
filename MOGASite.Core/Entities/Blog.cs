using MOGASite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string TitleAR { get; set; } = string.Empty;
        public string TitleEN { get; set; } = string.Empty;

        public string DescriptionAR { get; set; } = string.Empty;
        public string DescriptionEN { get; set; } = string.Empty;

        public string ContentAR { get; set; } = string.Empty;
        public string ContentEN { get; set; } = string.Empty;



        public string? ImageUrl { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Category Category { get; set; } 

        //public ICollection<BlogContent> BlogContents { get; set; } = new HashSet<BlogContent>();

    }
}
