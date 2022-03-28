using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
    }
}
