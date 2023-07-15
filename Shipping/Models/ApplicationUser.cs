using Microsoft.AspNetCore.Identity;

namespace Shipping.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public string? Date { get; set; }
    }


    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool Status { get; set; } = true;


        public List<Employee>? Employees { get; set; }

        public List<Merchant>? Merchants { get; set; }

        public List<Delivery>? Deliveries { get; set; }
    }
    
    public class ApplicationRoleCliams : IdentityRoleClaim<string>
    {
        public string? ArabicName { get; set; }

     
    }


}
