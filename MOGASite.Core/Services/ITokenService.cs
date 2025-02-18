﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager);
    }
}
