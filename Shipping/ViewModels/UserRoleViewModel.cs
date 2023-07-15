using Shipping.Models;

namespace Shipping.ViewModels
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }

    }
}
