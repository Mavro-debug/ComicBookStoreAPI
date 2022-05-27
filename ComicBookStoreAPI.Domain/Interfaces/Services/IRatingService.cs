using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Models;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IRatingService
    {
        List<RatingDto> GetAll(int comicBookId);
        RatingDto GetById(int comicBookId, int id);
        int Create(int comicBookId, ApplicationUser user, RatingDto ratingDto);
    }
}
