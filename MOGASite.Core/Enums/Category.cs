using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Enums
{
    public enum Category
    {
        [EnumMember(Value = "Web Development")]
        WebDevelopment,

        [EnumMember(Value = "Mobile Development")]
        MobileDevelopment,

        [EnumMember(Value = "CyberSecurity")]
        CyberSecurity,

        [EnumMember(Value = "Network")]
        Network,

        [EnumMember(Value = "Cloud Computing")]
        CloudComputing,

        [EnumMember(Value = "Hosting")]
        Hosting,

        [EnumMember(Value = "Digital Marketing")]
        DigitalMarketing,

        [EnumMember(Value = "Surveillance Systems")]
        SurveillanceSystems,

        [EnumMember(Value = "Computers")]
        Computers,

        [EnumMember(Value = "Copiers")]
        Copiers,

        [EnumMember(Value = "Fire Fighting")]
        FireFighting,

        [EnumMember(Value = "Queue Management")]
        QueueManagement,

    }
}
