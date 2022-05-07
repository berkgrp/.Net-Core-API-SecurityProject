using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EntityLayer.Models
{
    public class RoleGroup
    {
        [Key]
        public int RoleGroupID { get; set; }
        public string GroupName { get; set; }

        #region /*FluentApi Objects*/
        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
    public class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup>
    {
        public void Configure(EntityTypeBuilder<RoleGroup> builder)
        {
            builder.HasKey(user => user.RoleGroupID);

            builder.HasData(new RoleGroup
            {
                RoleGroupID = 1,
                GroupName = "User"
            });
        }
    }
}
