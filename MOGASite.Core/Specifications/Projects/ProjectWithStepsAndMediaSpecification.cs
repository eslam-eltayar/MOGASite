using MOGASite.Core.Entities;
using MOGASite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Specifications.Projects
{
    public class ProjectWithStepsAndMediaSpecification : BaseSpecification<Project>
    {
        public ProjectWithStepsAndMediaSpecification(int projectId)
            : base(p => p.Id == projectId)
        {
            AddIncludes();
        }

        public ProjectWithStepsAndMediaSpecification()
        {
            ApplyOrderByDescending(p => p.Id);

            AddIncludes();
        }

        public ProjectWithStepsAndMediaSpecification(string category)
            : base(p => (string.IsNullOrEmpty(category) || p.Category == Enum.Parse<Category>(category)))
        {
            ApplyOrderByDescending(p => p.Id);

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
