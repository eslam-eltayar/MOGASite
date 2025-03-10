﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services.Mail
{
    public class MailContent
    {
        public string To { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public string Subject { get; init; }
        public string Body { get; set; }
        public bool IsHTMLBody { get; set; }
    }
}
