using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Specifications.Projects
{
    public class ProjectBySlugSpecification : BaseSpecification<Project>
    {
        public ProjectBySlugSpecification(string slug) 
            : base(x => x.Slug == slug)
        {
            AddIncludes();
        }


        private void AddIncludes()
        {

            Includes.Add(p => p.MediaItems);
            Includes.Add(p => p.ProjectSteps);
            //Includes.Add(p => p.ProjectReviews);

        }
    }
}
