using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Shipping.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        public string Address { get; set; }
        public string Government { get; set; }
        public string City { get; set; }
        public int PickUpSpecialCost { get; set; }
        public int RefusedOrderPercent { get; set; }
        public virtual ApplicationUser User { set; get; }
        public virtual List<SpecialCitiesPrice>? SpecialCitiesPrices { get; set; }
        public virtual Branch Branch { get; set; }


    }
}
