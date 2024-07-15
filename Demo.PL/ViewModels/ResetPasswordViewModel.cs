using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage = "Minimum Length is 7")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm your Password.")]
        [Compare("NewPassword", ErrorMessage = "Password Doesn't match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
