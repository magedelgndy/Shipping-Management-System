using Shipping.Models;

namespace Shipping.ViewModels
{
    public class AsignDeliveryViewModel
    {
        public int? DeliveryId { get; set; }
        public int? BranchId { get; set; }
        public int? DeliveryName { get; set; }

        public List<Delivery>? deliveries { get; set; }

    }
}
