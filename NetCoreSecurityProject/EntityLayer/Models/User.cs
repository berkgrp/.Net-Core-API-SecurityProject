using Entities_HBKSOFTWARE.JwtModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Models
{
    public class User
    {
        public User()
        {
            UserGuidID = Guid.NewGuid();
        }

        [Key]
        public int UserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserGuidID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserSurname { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserPassword { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        [Required(ErrorMessage = "This field is required.")]
        public string UserEmail { get; set; }

        public virtual List<RefreshToken> RefreshTokens { get; set; }

        public List<string> UserRolesAsString { get; set; } = new List<string>();
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.UserID);//Primary Key Yapılandırması

            builder.Property(x => x.UserRolesAsString)
                   .HasConversion(new ValueConverter<List<string>, string>(
                    v => Newtonsoft.Json.JsonConvert.SerializeObject(v),
                    v => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(v)));

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
