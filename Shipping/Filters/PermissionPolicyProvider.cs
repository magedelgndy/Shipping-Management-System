using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace Shipping.Filters
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {

        public DefaultAuthorizationPolicyProvider FallbackPlicyProvider { get; }


        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPlicyProvider = new DefaultAuthorizationPolicyProvider(options);

        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallbackPlicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return FallbackPlicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permissions", StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            
            }

            return FallbackPlicyProvider.GetPolicyAsync(policyName);
        }
    }
}
