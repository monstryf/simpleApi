

using Microsoft.EntityFrameworkCore;
using simpleApi.Data;
using simpleApi.Model;

namespace simpleApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AddressModel> Address { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
