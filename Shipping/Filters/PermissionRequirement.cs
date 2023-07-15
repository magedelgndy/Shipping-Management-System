using Microsoft.AspNetCore.Authorization;

namespace Shipping.Filters
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string Permission { get; set; }
        public PermissionRequirement(string permission)
        {
            Permission = permission;
     
        }
    }
}
