using Shipping.Models;
using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم العميل ")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [RegularExpression(@"^1[0125][0-9]{8}$", ErrorMessage = "ادخل رقم هاتف صحيح كالاتى : 1224479550")]
        public int ClientPhoneNumber1 { get; set; }
        [RegularExpression(@"^1[0125][0-9]{8}$", ErrorMessage = "ادخل رقم هاتف صحيح كالاتى : 1224479550")]
        public int? ClientPhoneNumber2 { get; set; }
        [Required(ErrorMessage = "يجب ادخال البريد الالكتروني")]
        [RegularExpression(@"^\S+@\S+\.com$", ErrorMessage = "ادخل بريد الكتروني صحيح")]
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = " يجب ادخال السعر الكلى للطلب ")]
        public int OrderCost { get; set; }
        [Required(ErrorMessage = "يجب ادخال الوزن الكلى و ذلك بإضافه منتاجات")]
        public int TotalWeight { get; set; } =0;
        public bool IsVillage { get; set; } = false;
        [Required(ErrorMessage = "يجب ادخال اسم المحافظة")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم المدينة")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم الفرع")]
        public string BranchName { get; set; }
        public DateTime? OrderDate { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم الشارع")]
        public string StreetName { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }
        public int ShippingCost { get; set; }
        public int? TotalCost { get; set; } 
        public List<OrderProduct> orderProducts { get; set; } = new List<OrderProduct>();

        /* hussen */

        public int? BranchId { get; set; }

        public int? DeliveryId { get; set; }

        public int? MerchantId { get; set; }


        /* hussen*/

        public Models.Type Type { get; set; }
        public ShippingType ShippingType { get; set; }
        public PaymentType PaymentType { get; set; }
        public string? OrderStatus { get; set; }

    }
}
