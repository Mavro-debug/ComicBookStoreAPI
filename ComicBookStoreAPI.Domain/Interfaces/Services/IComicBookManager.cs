using ComicBookStoreAPI.Domain.Models;


namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IComicBookManager
    {
        IEnumerable<ComicBookCardDto> GetAll(string searchedPhrase);
        int CreateComicBook(NewComicBookDto newComicBook);
        void RemoveComicBook(int id);
        void UpdateComicBook(int comicBookId, NewComicBookDto newComicBook);
    }
}
