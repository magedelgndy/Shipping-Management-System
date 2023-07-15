using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Models
{
    public class Employee
    {
        public int Id { get; set; } 
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public  virtual ApplicationUser User { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
