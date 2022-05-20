using ComicBookStoreAPI.Domain.Models;


namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IComicBooksService
    {
        IEnumerable<ComicBookCardDto> GetAllCards();
        IEnumerable<ComicBookCardDto> GetAllSearchedCards(string searchedPhrase);
        ComicBookDescFeatDto GetAllComicBookDescFeats();
        int CreateComicBook(NewComicBookDto newComicBook);
        void RemoveComicBook(int id);
        void UpdateComicBook(int comicBookId, NewComicBookDto newComicBook);
    }
}
