namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasOne(v => v.Brand)
                   .WithMany(b => b.Vehicles)
                   .HasForeignKey(v => v.BrandId);

            builder.HasOne(v => v.VehicleType)
                   .WithMany(vt => vt.Vehicles)
                   .HasForeignKey(v => v.VehicleTypeId);

            builder.HasOne(v => v.Owner)
                   .WithMany(u => u.Vehicles)
                   .HasForeignKey(v => v.OwnerId);

            builder.HasIndex(v => v.PlateNumber)
                   .IsUnique();
        }
    }
}