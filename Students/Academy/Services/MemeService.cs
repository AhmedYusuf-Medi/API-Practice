using Api.Models.Base;
using Api.Models.Request.Meme;
using Api.Models.Response.MemeApi;
using AutoMapper;
using Data.Models;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace Services
{
    public class MemeService : BaseService<Meme>, IMemeService
    {
        public MemeService(AcademyContext academyContext, IMapper mapper)
            : base(academyContext, mapper)
        {
        }

        public async Task<Result<List<MemeResponseModel>?>> GetAllAsync(CancellationToken cancellationToken) =>
            new Result<List<MemeResponseModel>?>
            {
                IsSuccess = true,
                Message = string.Format("Successfully retrieved {0}!", nameof(_dbContext.Memes)),
                Payload = await AllAsNoTracking().Select(x => new MemeResponseModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    PostLink = x.PostLink,
                    Url = x.Url,
                    RedditPostLikes = x.RedditPostLikes,
                    IsNotSafeForWork = x.IsNotSafeForWork,
                    IsSpoiler = x.IsSpoiler,
                    PreviewLinks = x.PreviewLinks.Select(x => new MemePreviewLinkResponseModel { Url = x.Url }).ToList()
                }).ToListAsync(cancellationToken)
            };

        public async Task<InfoResult> CreateAsync(CreateMemeRequestModel request)
        {
            var meme = _mapper.Map<Meme>(request);

            await base.AddAsync(meme);
            await base.SaveChangesAsync();

            var result = new InfoResult
            {
                Message = "Successfully created Meme!",
                IsSuccess = true
            };

            return result;
        }
    }
}
