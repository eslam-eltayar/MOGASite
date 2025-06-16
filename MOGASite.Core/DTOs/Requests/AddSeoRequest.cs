using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Requests;
public class AddSeoRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string Route { get; set; }
    public string OgTitle { get; set; }
    public string OgDescription { get; set; }
    public IFormFile? OgImage { get; set; }
}