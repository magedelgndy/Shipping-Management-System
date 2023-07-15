using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "يجب إدخال الرقم السري القديم")]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "يجب إدخال الرقم السري الجديد")]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "الرقم السري غير متطابق")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
