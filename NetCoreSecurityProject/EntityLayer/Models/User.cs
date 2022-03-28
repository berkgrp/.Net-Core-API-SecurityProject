using Entities_HBKSOFTWARE.JwtModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserSurname { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserPassword { get; set; }

        [EmailAddress(ErrorMessage ="Please enter a valid email."),Required(ErrorMessage = "This field is required.")]
        public string UserEmail { get; set; }

        public virtual List<RefreshToken> RefreshTokens { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.UserID);//Primary Key Yapılandırması

            //Data Seeding
            builder.HasData(new User
            {
                UserID = 1,
                UserName = "Admin",
                UserSurname = "Admin",
                UserPassword = "AdminGitHub",
                UserEmail = "AdminPaneli@gmail.com",
            });
            builder.HasData(new User
            {
                UserID = 2,
                UserName = "Kullanici",
                UserSurname = "Kullanici",
                UserPassword = "KullaniciGitHub",
                UserEmail = "Kullanici@gmail.com",
            });
        }
    }
}
