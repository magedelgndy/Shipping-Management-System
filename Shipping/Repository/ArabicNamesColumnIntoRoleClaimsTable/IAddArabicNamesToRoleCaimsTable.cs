using Shipping.Models;

namespace Shipping.Repository.ArabicNamesColumnIntoRoleClaimsTable
{
    public interface IAddArabicNamesToRoleCaimsTable
    {
        bool AddArabicNamesToRoleCaims(ApplicationRole role, string ArabicName, string claimValue);
    }
}
