using Microsoft.EntityFrameworkCore;
using Shipping.Models;

namespace Shipping.Repository.ArabicNamesColumnIntoRoleClaimsTable
{
    public class AddArabicNamesToRoleCaimsTable : IAddArabicNamesToRoleCaimsTable
    {
        public MyContext context;
        public AddArabicNamesToRoleCaimsTable(MyContext _context)
        {
            context = _context;
        }

        public bool AddArabicNamesToRoleCaims(ApplicationRole role, string ArabicName, string claimValue)
        {
            var roleClaim = context.RoleClaims.FirstOrDefault(rc => rc.RoleId == role.Id && rc.ClaimValue == claimValue);
            if (roleClaim == null)
                return false;

            roleClaim.ArabicName = ArabicName;
            context.SaveChanges();
            return true;
        }
    }
}
