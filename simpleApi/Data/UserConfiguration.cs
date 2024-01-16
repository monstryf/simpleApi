using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simpleApi.Model;

namespace simpleApi.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.Email).IsRequired();
            builder.Property(x=>x.Password).IsRequired();
            builder.Property(x=>x.AddressId).IsRequired(false);
            
            
        }
    }
}
