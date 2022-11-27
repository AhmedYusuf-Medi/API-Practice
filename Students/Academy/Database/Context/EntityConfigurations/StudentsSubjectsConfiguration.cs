using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Context.EntityConfigurations
{
    internal class StudentsSubjectsConfiguration : IEntityTypeConfiguration<StudentsSubjects>
    {
        public void Configure(EntityTypeBuilder<StudentsSubjects> builder)
        {
            builder.HasKey(x => new { x.KeyA, x.KeyB });

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Subjects)
                .HasForeignKey(x => x.KeyA);

            builder.HasOne(x => x.Subject)
               .WithMany(x => x.Students)
               .HasForeignKey(x => x.KeyB);
        }
    }
}
