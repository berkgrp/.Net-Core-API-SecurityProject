using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address.")]
        public string UserEMail { get; set; }

        [Required(ErrorMessage="This field is required."),DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}
