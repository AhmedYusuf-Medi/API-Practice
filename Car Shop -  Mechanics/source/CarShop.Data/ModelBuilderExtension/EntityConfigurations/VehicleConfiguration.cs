namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasOne(vehicle => vehicle.Brand)
                   .WithMany(brand => brand.Vehicles)
                   .HasForeignKey(vehicle => vehicle.BrandId);

            builder.HasOne(vehicle => vehicle.VehicleType)
                   .WithMany(vehicleType => vehicleType.Vehicles)
                   .HasForeignKey(vehicle => vehicle.VehicleTypeId);

            builder.HasOne(vehicle => vehicle.Owner)
                   .WithMany(user => user.Vehicles)
                   .HasForeignKey(vehicle => vehicle.OwnerId);

            builder.HasIndex(vehicle => vehicle.PlateNumber)
                   .IsUnique();
        }
    }
}