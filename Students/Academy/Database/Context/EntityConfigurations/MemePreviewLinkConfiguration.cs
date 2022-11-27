using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations
{
    internal class MemePreviewLinkConfiguration : IEntityTypeConfiguration<MemePreviewLink>
    {
        public void Configure(EntityTypeBuilder<MemePreviewLink> builder)
        {
            builder.HasOne(x => x.Meme)
                .WithMany(x => x.PreviewLinks)
                .HasForeignKey(x => x.MemeId);

            builder.Property(memePreviewLink => memePreviewLink.Url).IsRequired().HasMaxLength(2000);
        }
    }
}
