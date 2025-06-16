using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Responses;
public class SeoResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string Route { get; set; }
    public string OgTitle { get; set; }
    public string OgDescription { get; set; }
    public string OgImage { get; set; }
}