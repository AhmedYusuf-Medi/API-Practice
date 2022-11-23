using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations
{
    public class LecturersSubjectsConfiguration : IEntityTypeConfiguration<LecturersSubjects>
    {
        public void Configure(EntityTypeBuilder<LecturersSubjects> builder)
        {
            builder.HasKey(x => new { x.KeyA, x.KeyB });

            builder.HasOne(x => x.Lecturer)
                .WithMany(x => x.Subjects)
                .HasForeignKey(x => x.KeyA);

            builder.HasOne(x => x.Subject)
               .WithMany(x => x.Lecturers)
               .HasForeignKey(x => x.KeyB);
        }
    }
}
