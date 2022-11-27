using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations
{
    internal class MemeConfiguration : IEntityTypeConfiguration<Meme>
    {
        public void Configure(EntityTypeBuilder<Meme> builder)
        {
            builder.Property(meme => meme.Title).IsRequired().HasMaxLength(200);
            builder.Property(meme => meme.Url).IsRequired().HasMaxLength(2000);
            builder.Property(meme => meme.Author).IsRequired().HasMaxLength(100);
        }
    }
}
