using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Models
{
    public class City
    {
        public int Id { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        public string Name { get; set; }
        public int ShippingPrice { get; set; }
        public int PickUpPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool Status { get; set; } = true;

        public State? State { get; set; }
    }
}
