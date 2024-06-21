using EMicroservices.IDP.Infrastructure.Common;
using EMicroservices.IDP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMicroservices.IDP.Entities.Configuration
{
    public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
    {




        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", SystemConstants.IdentitySchema).HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.RoleId, x.Function, x.Command }).IsUnique();

            //function-Product / RoleId-Admin/ Command-Add
        }


    }
}
