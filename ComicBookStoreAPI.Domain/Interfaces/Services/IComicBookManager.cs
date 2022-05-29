using ComicBookStoreAPI.Domain.Models;


namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IComicBookManager
    {
        IEnumerable<ComicBookDto> GetAll(string searchedPhrase);
        ComicBookDto GetById(int id);
        int CreateComicBook(NewComicBookDto newComicBook);
        void RemoveComicBook(int id);
        void UpdateComicBook(int comicBookId, NewComicBookDto newComicBook);
    }
}
