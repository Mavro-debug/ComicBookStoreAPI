using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Models;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IRatingManager
    {
        List<RatingDto> GetAll(int comicBookId);
        RatingDto GetById(int comicBookId, int id);
        Task<int> Create(int comicBookId, ApplicationUser user, CreateRatingDto ratingDto);
        Task<int> Change(int comicBookId, ApplicationUser user, CreateRatingDto ratingDto);
    }
}
