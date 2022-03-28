using System.ComponentModel.DataAnnotations.Schema;

namespace Entities_HBKSOFTWARE.JwtModels
{
    [NotMapped]
    public class JWTSettings
    {
        public string SecretKey { get; set; }
    }
}
