using CarShop.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.HasOne(i => i.Status)
                   .WithMany(s => s.Issues)
                   .HasForeignKey(i => i.StatusId);

            builder.HasOne(i => i.Priority)
                   .WithMany(p => p.Issues)
                   .HasForeignKey(i => i.PriorityId);

            builder.HasOne(i => i.Vehicle)
                   .WithMany(v => v.Issues)
                   .HasForeignKey(i => i.VehicleId);


        }
    }
}
