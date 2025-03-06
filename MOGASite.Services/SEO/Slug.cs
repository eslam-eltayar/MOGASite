using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services.SEO
{
    public static class Slug
    {
        public static string GenerateSlug(string title)
        {
            return title.ToLower()
                        .Replace(" ", "-")       // Replace spaces with hyphens
                        .Replace("?", "")        // Remove question marks
                        .Replace("&", "and")     // Replace ampersands
                        .Replace("/", "-")       // Replace slashes
                        .Replace("\"", "")       // Remove quotes
                        .Replace("'", "")        // Remove apostrophes
                        .Replace(".", "")        // Remove dots
                        .Trim();
        }
    }
}
