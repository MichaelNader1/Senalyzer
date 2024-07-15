using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage="UserName is Required.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name is Required.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage ="Invalid Email.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [MinLength(7 , ErrorMessage ="Minimum Length is 7")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your Password.")]
        [Compare("Password", ErrorMessage ="Password Doesn't match.")]
        public string ConfirmPassword { get; set; }
         
        public bool IsAgree  { get; set; }
    }
}
