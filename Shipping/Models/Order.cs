using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Models
{
    
    public class Order
    {
        [Key]
        public int SerialNumber { get; set; }
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }

        //[ForeignKey("State")]
        public int StateId { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        [ForeignKey("Delivery")]
        public int? DeliveryId { get; set; }

        [ForeignKey("Branch")]
        public int? BranchId { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public string ClientName { get; set; }
        public int ClientPhoneNumber1 { get; set; }
        public int? ClientPhoneNumber2 { get; set; }
        public string ClientEmail { get; set; }
        public int OrderCost { get; set; }
        public int TotalWeight { get; set; }
        public string OrderStatus { get; set; } ="جديد";
        public bool IsVillage { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public int ShippingCost { get; set; }
        public int TotalCost => OrderCost + ShippingCost;


        public string StreetName { get; set; }
        public string? Notes { get; set; }

        public Type Type { get; set; }
        public ShippingType ShippingType { get; set; }
        public PaymentType PaymentType { get; set; }


        public virtual Delivery? Delivery { get; set; }
        public virtual Branch? Branch { get; set; }

        public virtual Merchant? Merchant { get; set; }
        //public virtual State State { get; set; }
        public virtual City? City { get; set; }
        public virtual List<OrderProduct>? orderProducts { set; get; }

    }

    ////////////////////////////////Enums///////////////////////////////////////
    public enum Type
    {
        تسليم_فالفرع,
        توصيل_الي_المنزل
    }
    public enum ShippingType
    {
        توصيل_في_نفس_اليوم,
        توصيل_سريع,
        توصيل_عادي
    }
    public enum PaymentType
    {
        واجبة_التحصيل,
        دفع_مقدم,
        طرد_مقابل_طرد
    }

    public enum OrderStatus
    {
        جديد,
        قيد_الانتظار,
        تم_التسليم_للمندوب,
        تم_التسليم,
        لا_يمكن_الوصول,
        تم_التاجيل,
        تم_التسليم_جزئيا,
        تم_الالغاء_من_جهة_العميل,
        تم_الرفض_مع_الدفع,
        رفض_مع_سداد_جزء,
        رفض_ولم_يتم_الدفع
    }



}
