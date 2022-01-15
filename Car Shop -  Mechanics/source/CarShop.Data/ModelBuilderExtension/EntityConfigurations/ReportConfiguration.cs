namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasOne(report => report.Receiver)
                   .WithMany(user => user.ReceivedReports)
                   .HasForeignKey(report => report.ReceiverId);

            builder.HasOne(report => report.Sender)
                  .WithMany(user => user.SentReports)
                  .HasForeignKey(report => report.SenderId);

            builder.HasOne(report => report.ReportType)
              .WithMany(reportType => reportType.Reports)
              .HasForeignKey(report => report.ReportTypeId);
        }
    }
}