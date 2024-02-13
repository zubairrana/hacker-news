using AutoMapper;
using HackerNews.Domain.DTO;
using HackerNews.Domain.Models;

namespace HackerNews.BusinessLogic.Configuration
{
    public class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile() 
        {
            CreateMap<BestStoryModel, BestStory>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Kids.Count))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.PostedBy, opt => opt.MapFrom(src => src.By));
        }
    }
}
