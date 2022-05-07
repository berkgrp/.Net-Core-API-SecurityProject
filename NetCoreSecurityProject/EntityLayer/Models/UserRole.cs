using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntityLayer.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }

        public long? Roles { get; set; }

        #region /*Foreign key*/
        public int? UserID { get; set; }
        public int? RoleGroupID { get; set; }
        #endregion

        #region /*FluentApi Objects*/
        [JsonIgnore]
        public virtual RoleGroup RoleGroup { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        #endregion


    }
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.UserRoleID);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserID);

            builder.HasOne(x => x.RoleGroup)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleGroupID);

            builder.HasData(new UserRole
            {
                UserRoleID = 1,
                UserID = 1,
                RoleGroupID = 1,
                Roles = 7
            });
            builder.HasData(new UserRole
            {
                UserRoleID = 2,
                UserID = 2,
                RoleGroupID = 1,
                Roles = 15
            });
        }
    }
}
