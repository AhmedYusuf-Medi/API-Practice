using Api.Models.Request.Meme;
using AutoMapper;
using Data.Models;

namespace Services.Maps
{
    public class MemeProfile : Profile
    {
        public MemeProfile()
        {
            CreateMap<CreateMemeRequestModel, Meme>()
                    .ForMember(destination => destination.IsSpoiler, option => option.MapFrom(source => source.IsSpoiler))
                    .ForMember(destination => destination.IsNotSafeForWork, option => option.MapFrom(source => source.IsNotSafeForWork))
                    .ForMember(destination => destination.Author, option => option.MapFrom(source => source.Author))
                    .ForMember(destination => destination.Url, option => option.MapFrom(source => source.Url));
        }
    }
}
