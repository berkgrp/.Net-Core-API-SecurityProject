using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntityLayer.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public long? RoleIDForBitwise { get; set; }
        public string RoleName { get; set; }

        #region /*Foreign key*/
        public int? RoleGroupID { get; set; }
        #endregion

        #region /*FluentApi Objects*/
        [JsonIgnore]
        public virtual RoleGroup RoleGroup { get; set; }
        #endregion
    }
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(user => user.RoleID);

            builder.HasOne(x => x.RoleGroup)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.RoleGroupID);

            builder.HasData(new Role
            {
                RoleID = 1,
                RoleIDForBitwise = 1,
                RoleName = "GetUser",
                RoleGroupID = 1
            });

            builder.HasData(new Role
            {
                RoleID = 2,
                RoleIDForBitwise = 2,
                RoleName = "Get",
                RoleGroupID = 1
            });

            builder.HasData(new Role
            {
                RoleID = 3,
                RoleIDForBitwise = 4,
                RoleName = "Post",
                RoleGroupID = 1
            });

            builder.HasData(new Role
            {
                RoleID = 4,
                RoleIDForBitwise = 8,
                RoleName = "Update",
                RoleGroupID = 1
            });
        }
    }
}
