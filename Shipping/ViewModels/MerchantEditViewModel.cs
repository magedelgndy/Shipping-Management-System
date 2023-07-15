using Shipping.Models;
using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels
{
    public class MerchantEditViewModel
    {
        public string? MerchantId { get; set; }
        [Required(ErrorMessage = "يجب ادخال الاسم")]
        [MinLength(10, ErrorMessage = "ادخل الاسم ثلاثي")]
        public string Name { get; set; }
        [Required(ErrorMessage = "يجب ادخال البريد الالكتروني")]
        [RegularExpression(@"^\S+@\S+\.com$", ErrorMessage = "ادخل بريد الكتروني صحيح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [RegularExpression(@"^1[0125][0-9]{8}$", ErrorMessage = "ادخل رقم هاتف صحيح كالاتى : 1224479550")]
        public string Phone { get; set; }
        public string BranchName { get; set; }
        [Required(ErrorMessage = "يجب ادخال العنوان")]
        public string Address { get; set; }
        [Required(ErrorMessage = "يجب ادخال المدينة")]
        public string City { get; set; }
        [Required(ErrorMessage = "يجب ادخال المحافظة")]
        public string Government { get; set; }
        [Required(ErrorMessage = "يجب ادخال سعر pickup")]
        public int PickUpPrice { get; set; }
        [Required(ErrorMessage = "يجب ادخال نسبة تحمل التاجر للرفض")]
        public int RefuseCostPercent { get; set; }
        public List<SpecialCitiesPrice>? SpecialCities { get; set; }


    }
}
