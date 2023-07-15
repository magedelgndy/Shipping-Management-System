using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int Weight { get; set; }

        [ForeignKey(nameof(Orders))]
        public int OrderId { get; set; }
        public virtual Order? Orders { get; set; }
    }
}
