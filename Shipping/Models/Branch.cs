using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [ForeignKey("State")]
        [Required(ErrorMessage = "يجب ادخال المحافظة")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "يجب ادخال الاسم الفرع")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDeleted { get; set; } = false;


        public virtual State State { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Employee> Employees { get; set; }
        public virtual List<Merchant> Merchants { get; set; }

    }
}
