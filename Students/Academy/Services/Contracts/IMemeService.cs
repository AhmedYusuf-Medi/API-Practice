using Api.Models.Base;
using Api.Models.Request.Meme;
using Api.Models.Response.MemeApi;

namespace Services.Contracts
{
    public interface IMemeService
    {
        Task<InfoResult> CreateAsync(CreateMemeRequestModel request);
        Task<Result<List<MemeResponseModel>?>> GetAllAsync(CancellationToken cancellationToken);
    }
}