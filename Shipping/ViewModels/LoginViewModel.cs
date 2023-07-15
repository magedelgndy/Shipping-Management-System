using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "يجب ادخال البريد الالكترونى")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "يجب ادخال الرقم السري")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
