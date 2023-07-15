using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Models
{
    public enum DiscountType {
        Percentage,
        Value
    }
    public class Delivery
    {
        public int Id { get; set; }
        public string Governement { get; set; }
        public string Address { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public DiscountType DiscountType { get; set; }
        public int CompanyPercent { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
