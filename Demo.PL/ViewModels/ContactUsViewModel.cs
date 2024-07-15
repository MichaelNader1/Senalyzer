using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "Please enter your full name.")]
        public string FullName { get; set; }

        [Key]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }
    }
}
