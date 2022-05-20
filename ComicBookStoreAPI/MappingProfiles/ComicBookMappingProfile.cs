using AutoMapper;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Models;

namespace ComicBookStoreAPI.MappingProfiles
{
    public class ComicBookMappingProfile : Profile
    {
        public ComicBookMappingProfile()
        {
            CreateMap<ComicBook, ComicBookCardDto>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.Price, c => c.MapFrom(s => s.Price))
                .ForMember(m => m.Plot, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.PosterPath, c => c.MapFrom(
                    s => s.Posters.Where(p => p.Cover == true).FirstOrDefault().Path));

            CreateMap<DateDto, DateTime>()
                .ForMember(m => m.Year, c => c.MapFrom(s => s.Year))
                .ForMember(m => m.Month, c => c.MapFrom(s => s.Month))
                .ForMember(m => m.Day, c => c.MapFrom(s => s.Day));
        }
    }
}
