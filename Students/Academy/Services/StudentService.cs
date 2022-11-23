using Api.Models.Base;
using Api.Models.Request;
using Api.Models.Response;
using AutoMapper;
using Data.Models;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Services.Common;
using Services.Contracts;

namespace Services
{
    public class StudentService : BaseService<Student>, IStudentService
    {
        private readonly IMapper _mapper;

        public StudentService(AcademyContext academyContext, IMapper mapper)
            : base(academyContext)
        {
            this._mapper = mapper ?? throw new ArgumentNullException();
        }

        public async Task<Result<List<StudentResponseModel>?>> GetAllAsync(CancellationToken cancellationToken) =>
            new Result<List<StudentResponseModel>?>
            {
                IsSuccess = true,
                Message = string.Format("Successfully retrieved {0}!", nameof(dbContext.Students)),
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

        public async Task<InfoResult> CreateAsync(CreateStudentRequest request)
        {
            var student = _mapper.Map<Student>(request);

            await base.AddAsync(student);
            await base.SaveChangesAsync();

            var result = new InfoResult
            {
                Message = "Successfully created Student!",
                IsSuccess = true
            };

            return result;
        }

        public async Task<InfoResult> UpdateAsync(Guid id, UpdateStudentRequest request)
        {
            var student = await All().SingleOrDefaultAsync(x => x.Id == id);

            _mapper.Map(student, request);

            await base.SaveChangesAsync();

            var result = new InfoResult
            {
                Message = "Successfully update Student!",
                IsSuccess = true
            };

            return result;
        }

        public async Task<InfoResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var student = await All().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Common.KeyNotFoundException("Student doesn't exists!");
            base.Delete(student);
            await base.SaveChangesAsync(cancellationToken);

            var result = new InfoResult
            {
                Message = "Successfully deleted Student!",
                IsSuccess = true
            };

            return result;
        }

        public async Task<InfoResult> AddSubjectToStudentAsync(AddSubjectToStudentRequest request)
        {
            if (await dbContext.StudentsSubjects.AnyAsync(x => x.KeyA == request.StudentId && x.KeyB == request.SubjectId))
            {
                throw new BadRequestException("Student already goes to the selected subject!");
            }

            var studentId = await AllAsNoTracking().Select(x => x.Id).SingleOrDefaultAsync(x => x == request.StudentId);

            if (studentId == null)
            {
                throw new Common.KeyNotFoundException("Student doesn't exists!");
            }

            var subjectId = await dbContext.Subjects.AsNoTracking().Select(x => x.Id).SingleOrDefaultAsync(x => x == request.SubjectId);

            if (subjectId == null)
            {
                throw new Common.KeyNotFoundException("Subject doesn't exists!");
            }

            var studentSubject = new StudentsSubjects
            {
                KeyA = request.StudentId,
                KeyB = request.SubjectId
            };

            await dbContext.StudentsSubjects.AddAsync(studentSubject);
            await base.SaveChangesAsync();

            var result = new InfoResult
            {
                Message = "Successfully linked subject to the selected student!",
                IsSuccess = true
            };

            return result;
        }

        public void ThrowServerError()
            => throw new NotImplementedException("INTERNAL SERVER ERROR!");
    }
}
