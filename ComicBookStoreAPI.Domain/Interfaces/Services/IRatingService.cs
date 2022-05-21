using ComicBookStoreAPI.Domain.Entities;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IRatingService
    {
        Rating GetAllWithComicBookId(int comicBookId);
    }
}
