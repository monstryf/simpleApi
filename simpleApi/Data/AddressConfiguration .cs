using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simpleApi.Model;

namespace simpleApi.Data
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressModel>
    {
        public void Configure(EntityTypeBuilder<AddressModel> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Location).IsRequired();
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x=>x.Latitude).IsRequired();
            builder.Property(x=>x.Longitude).IsRequired();
            builder.HasOne(x=>x.User).WithOne(x => x.Address).HasForeignKey<UserModel>(x=>x.AddressId).IsRequired(false);
        }
    }
}
