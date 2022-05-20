using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookStoreAPI.Controllers
{

    [Route("comicBooks")]
    public class ComicBooksController : ControllerBase
    {
        private readonly IComicBooksService _comicBooksService;

        public ComicBooksController(IComicBooksService comicBooksService)
        {
            _comicBooksService = comicBooksService;
        }

        [HttpGet]
        [Route("getAllCards")]
        public IActionResult GetAllCards()
        {
            var cards = _comicBooksService.GetAllCards();

            return Ok(cards);
        }

        [HttpGet]
        [Route("getAllSearchedCards")]
        public IActionResult GetAllSearchedCards([FromQuery] string searchedPhrase)
        {
            var cards = _comicBooksService.GetAllSearchedCards(searchedPhrase);

            return Ok(cards);
        }

        [HttpGet]
        [Route("getComicBookDescFeats")]
        public IActionResult GetComicBookDescFeats()
        {
            ComicBookDescFeatDto descFeat = _comicBooksService.GetAllComicBookDescFeats();

            return Ok(descFeat);
        }

        [HttpPost]
        [Route("addComicBook")]
        public IActionResult AddComicBook([FromBody] NewComicBookDto newComicBookDto)
        {
            int newComicBookId = _comicBooksService.CreateComicBook(newComicBookDto);

            return Ok(newComicBookId);
        }

        [HttpDelete]
        [Route("removeComicBook/{id}")]
        public IActionResult RemoveComicBook([FromRoute] int id)
        {
            _comicBooksService.RemoveComicBook(id);

            return NotFound();
        }

        [HttpPatch]
        [Route("updateComicBook/{id}")]
        public IActionResult UpdateComicBook([FromRoute] int id, [FromBody] NewComicBookDto newComicBookDto)
        {
            _comicBooksService.UpdateComicBook(id, newComicBookDto);

            return Ok();
        }

    }
}
