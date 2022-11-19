using Api.Models.Base;
using Api.Models.Response;
using Data.Models;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.Common;

namespace Services
{
    public class StudentService : BaseService<Student>, IStudentService
    {
        public StudentService(AcademyContext academyContext)
            : base(academyContext)
        {
        }

        public async Task<Result<List<StudentResponseModel>?>> GetAllAsync(CancellationToken cancellationToken) =>
            new Result<List<StudentResponseModel>?>
            {
                IsSuccess = true,
                Message = "Successfully retrieved students!",
                Payload = await AllAsNoTracking().Select(x => new StudentResponseModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    SubjectsCount = x.Subjects!.Count,
                    RegisteredOn = x.CreatedOn
                }).ToListAsync(cancellationToken)
            };

        public async Task<Result<StudentResponseModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            new Result<StudentResponseModel>
            {
                IsSuccess = true,
                Message = "Successfully retrieved student!",
                Payload = await AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new StudentResponseModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    SubjectsCount = x.Subjects!.Count,
                    RegisteredOn = x.CreatedOn
                }).SingleOrDefaultAsync(cancellationToken) ?? throw new Common.KeyNotFoundException("Student doesn't exists!")
            };

        public void ThrowServerError()
            => throw new NotImplementedException("INTERNAL SERVER ERROR!");
    }
}
