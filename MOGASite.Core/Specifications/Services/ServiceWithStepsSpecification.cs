using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.Entities;
using MOGASite.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Specifications.Services
{
    public class ServiceWithStepsSpecification : BaseSpecification<Service>
    {
        public ServiceWithStepsSpecification(int serviceId)
            : base(x => x.Id == serviceId)
        {
            AddInclude();
        }

        public ServiceWithStepsSpecification()
        {
            AddInclude();
        }

        public ServiceWithStepsSpecification(ServiceByCategoryRequest request)
        : base(s => (string.IsNullOrEmpty(request.Category) || s.Category == request.Category))
        {
            AddInclude();
        }

        public ServiceWithStepsSpecification(string slug)
            : base(s => s.Slug == slug)
        {
            AddInclude();
        }

        private void AddInclude()
        {
            Includes.Add(x => x.ServiceSteps);
        }
    }
}
