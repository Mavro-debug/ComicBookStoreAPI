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

            CreateMap<ComicBook, ComicBookDto>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.Price, c => c.MapFrom(s => s.Price))
                .ForMember(m => m.Edition, c => c.MapFrom(s => s.Edition))
                .ForMember(m => m.ReleaseDate, c => c.MapFrom(s => s.ReleaseDate))
                .ForMember(m => m.Screenwriter, c => c.MapFrom(s => s.Screenwriter))
                .ForMember(m => m.Series, c => c.MapFrom(s => s.Series))
                .ForMember(m => m.Ratings, c => c.MapFrom(s => s.Ratings))
                .ForMember(m => m.NumberOfPages, c => c.MapFrom(s => s.NumberOfPages))
                .ForMember(m => m.CoverType, c => c.MapFrom(s => s.CoverType))
                .ForMember(m => m.Discount, c => c.MapFrom(s => s.Discount))
                .ForMember(m => m.Posters, c => c.MapFrom(s => s.Posters))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.Illustrators, c => c.MapFrom(s => s.ComicBookIllustrators))
                .ForMember(m => m.HeroesTeams, c => c.MapFrom(s => s.ComicBookHeroesTeams));


            CreateMap<ComicBookIllustrator, IllustratorDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Illustrator.Name))
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Illustrator.Id));

            CreateMap<ComicBookHeroesTeams, HeroesTeamsDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.HeroesTeams.Name))
                .ForMember(m => m.Id, c => c.MapFrom(s => s.HeroesTeams.Id));

            CreateMap<DateDto, DateTime>()
                .ForMember(m => m.Year, c => c.MapFrom(s => s.Year))
                .ForMember(m => m.Month, c => c.MapFrom(s => s.Month))
                .ForMember(m => m.Day, c => c.MapFrom(s => s.Day));

            CreateMap<Rating, RatingDto>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Grade, c => c.MapFrom(s => s.Grade))
                .ForMember(m => m.Commentary, c => c.MapFrom(s => s.Commentary));

            CreateMap<RatingDto, Rating>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Grade, c => c.MapFrom(s => s.Grade))
                .ForMember(m => m.Commentary, c => c.MapFrom(s => s.Commentary));

            CreateMap<ApplicationUser, UserDto>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.UserName));
        }
    }
}
