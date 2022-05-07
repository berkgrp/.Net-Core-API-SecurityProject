using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities_HBKSOFTWARE.JwtModels
{
    [NotMapped]
    public class UserWithToken 
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public Guid UserGuidID { get; set; }
    }
}
