namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasOne(issue => issue.Status)
                   .WithMany(status => status.Issues)
                   .HasForeignKey(issue => issue.StatusId);

            builder.HasOne(issue => issue.Priority)
                   .WithMany(priority => priority.Issues)
                   .HasForeignKey(issue => issue.PriorityId);

            builder.HasOne(issue => issue.Vehicle)
                   .WithMany(vehicle => vehicle.Issues)
                   .HasForeignKey(issue => issue.VehicleId);
        }
    }
}