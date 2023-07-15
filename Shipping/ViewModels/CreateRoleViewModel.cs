using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "يجب إدخال اسم المجموعه")]
        public string RoleName { get; set; }
    }
}
