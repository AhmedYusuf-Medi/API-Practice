using Api.Models.Base;
using Api.Models.Response.Student;

namespace Services.Contracts
{
    public interface IStudentService
    {
        Task<Result<List<StudentResponseModel>?>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<StudentResponseModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void ThrowServerError();
    }
}