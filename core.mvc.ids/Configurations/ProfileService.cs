﻿using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.mvc.ids.Configurations
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims = context.Subject.Claims.ToList();
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}
