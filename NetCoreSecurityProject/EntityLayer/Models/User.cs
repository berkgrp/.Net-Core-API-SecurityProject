using Entities_HBKSOFTWARE.JwtModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
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
                UserPassword = "CfDJ8E2g0tyToKBIorFTKMYz8oPTSA3c-MsZ06mFLAKsWpmoPPIfS1voqv2EdJ93wragYJn4vhYdKxJMQDQmnxWcFlaVauYDtA0MmRjVe55gNkAuP02MqWhA1OWm7qG5VrVq1g",//AdminGitHub
                UserEmail = "AdminPaneli@gmail.com",
            });
            builder.HasData(new User
            {
                UserID = 2,
                UserName = "Kullanici",
                UserSurname = "Kullanici",
                UserPassword = "CfDJ8E2g0tyToKBIorFTKMYz8oOKphk8vXOSJMpPmG2PSySdPXezpmXkpilJS7CpsPOwvzzcRavxb1wQlH71D3XsVc6Nkg-WKwTJyAjqi6CLjGiot3trnuefW6iASirXW7B2dw",//KullaniciGitHub
                UserEmail = "Kullanici@gmail.com",
            });
        }
    }
}
