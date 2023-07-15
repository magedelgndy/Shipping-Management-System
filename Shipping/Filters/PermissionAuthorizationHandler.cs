using Microsoft.AspNetCore.Authorization;

namespace Shipping.Filters
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            if (context.User == null)
                return; 

            var canAccess = context.User.Claims.Any(c => c.Type == "Permissions" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
