using ComicBookStoreAPI.Domain.Models;


namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IComicBookManager
    {
        IEnumerable<ComicBookCardDto> GetAllCards();
        IEnumerable<ComicBookCardDto> GetAllSearchedCards(string searchedPhrase);
        int CreateComicBook(NewComicBookDto newComicBook);
        void RemoveComicBook(int id);
        void UpdateComicBook(int comicBookId, NewComicBookDto newComicBook);
    }
}
