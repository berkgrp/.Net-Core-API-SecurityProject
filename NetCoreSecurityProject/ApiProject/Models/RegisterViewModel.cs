using EntityLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        public User User { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserAgainPassword { get; set; }
    }
}
