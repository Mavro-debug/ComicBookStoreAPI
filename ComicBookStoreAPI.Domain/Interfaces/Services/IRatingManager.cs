using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Models;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IRatingManager
    {
        List<RatingDto> GetAll(int comicBookId);
        RatingDto GetById(int comicBookId, int ratingId);
        Task<int> Create(int comicBookId, CreateRatingDto ratingDto);
        Task<int> Change(int comicBookId, int ratingId, CreateRatingDto ratingDto);
        Task Delete(int comicBookId, int ratingId);
    }
}
