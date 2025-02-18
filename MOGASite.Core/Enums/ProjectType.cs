using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Enums
{
    public enum ProjectType
    {
        [EnumMember(Value = "Software")]
        Software,

        [EnumMember(Value = "Hardware")]
        Hardware,
    }
}
