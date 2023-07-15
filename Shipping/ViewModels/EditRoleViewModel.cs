using System.ComponentModel.DataAnnotations;

namespace Shipping.ViewModels
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; }

        [Required(ErrorMessage = "يجب ادخال اسم المجموعه")]
        public string RoleName { get; set; }
        
    }
}
