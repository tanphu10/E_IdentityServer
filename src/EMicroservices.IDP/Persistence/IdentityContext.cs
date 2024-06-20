using EMicroservice.IDP.Entities;
using EMicroservice.IDP.Entities.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMicroservice.IDP.Persistence
{
    public class IdentityContext:IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {

        }
        public DbSet<Permission> Permissions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
            builder.ApplyIdentityConfiguration();

        }

    }
}
