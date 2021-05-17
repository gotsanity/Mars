using Mars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Infrastructure
{
    public class OwnershipAuthorizationRequirement : IAuthorizationRequirement
    {
        // authorization requirements
        public bool AllowOwners { get; set; }
    }

    public class OwnershipAuthorizationHandler : AuthorizationHandler<OwnershipAuthorizationRequirement>
    {
        private UserManager<IdentityUser> userManager;

        public OwnershipAuthorizationHandler(UserManager<IdentityUser> usrMgr)
        {
            userManager = usrMgr;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnershipAuthorizationRequirement requirement)
        {
            // authorization logic
            BlogPost post = context.Resource as BlogPost;

            string userId = userManager.GetUserId(context.User);

            StringComparison compare = StringComparison.OrdinalIgnoreCase;

            if (post != null &&
                userId != null &&
                (requirement.AllowOwners && post.UserId.Equals(userId, compare))
            ){
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
