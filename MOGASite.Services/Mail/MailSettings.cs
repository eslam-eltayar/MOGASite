using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services.Mail
{
    public class MailSettings
    {
        public string DisplayName { get; init; }
        public string Email { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
        public string? Password { get; init; }
    }
}
