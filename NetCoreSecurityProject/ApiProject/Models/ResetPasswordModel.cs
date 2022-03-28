using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Token { get; set; }
    }
}
