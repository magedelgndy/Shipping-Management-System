using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Models
{
    public class SpecialCitiesPrice
    {
        public int Id { get; set; }

        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }
        public string Government { get; set; }
        public string City { get; set; }
        public int Price { get; set; }

        public virtual Merchant? Merchant { get; set; }
    }
}
