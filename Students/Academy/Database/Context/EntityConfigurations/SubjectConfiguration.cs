using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasMany(x => x.Students)
                .WithMany(x => x.Subjects);

            builder.HasMany(x => x.Lecturers)
                .WithMany(x => x.Subjects);
        }
    }
}
