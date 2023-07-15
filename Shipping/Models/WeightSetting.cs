using System.ComponentModel.DataAnnotations;

namespace Shipping.Models
{
    public class WeightSetting
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "يجب إدخال تكلفة الشحن الافتراضي")]

        public int Cost { get; set; } = 10;
        [Required(ErrorMessage = "يجب إدخال سعر كل كجم إضافي بالجنية")]
        public int Addition_Cost { get; set; } = 100;
    }
}
