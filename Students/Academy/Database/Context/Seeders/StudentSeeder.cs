using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Context.Seeders
{
    public class StudentSeeder : ISeeder
    {
        public async Task SeedAsync(AcademyContext dbContext)
        {
            if (await dbContext.Students.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var students = new HashSet<(string FirstName, string LastName, DateTime BirthDate)>
            {
                ("Pecata", "Dimitrov", DateTime.Now),
                ("Rado", "Baniski", DateTime.Now),
            };

            foreach (var student in students)
            {
                await dbContext.Students.AddAsync(new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
