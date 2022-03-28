using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class LoginViewModel
    {

        [DataType(DataType.EmailAddress)]
        public string UserEMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}
