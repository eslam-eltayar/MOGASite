using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Specifications.Hostings
{
    public class HostingWithPropertiesSpecification : BaseSpecification<Hosting>
    {
        public HostingWithPropertiesSpecification()
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(x => x.HostingProperties);
        }
    }
}
