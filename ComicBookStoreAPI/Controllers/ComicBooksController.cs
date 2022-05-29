using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookStoreAPI.Controllers
{

    [Route("comicBooks")]
    public class ComicBooksController : ControllerBase
    {
        private readonly IComicBookManager _comicBooksManager;

        public ComicBooksController(IComicBookManager comicBooksManager)
        {
            _comicBooksManager = comicBooksManager;
        }



        [HttpGet]
        public IActionResult GetAll([FromQuery] string searchedPhrase)
        {
            var cards = _comicBooksManager.GetAllSearchedCards(searchedPhrase);

            return Ok(cards);
        }


        [HttpPost]
        public IActionResult AddComicBook([FromBody] NewComicBookDto newComicBookDto)
        {
            int newComicBookId = _comicBooksManager.CreateComicBook(newComicBookDto);

            return Ok(newComicBookId);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveComicBook([FromRoute] int id)
        {
            _comicBooksManager.RemoveComicBook(id);

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateComicBook([FromRoute] int id, [FromBody] NewComicBookDto newComicBookDto)
        {
            _comicBooksManager.UpdateComicBook(id, newComicBookDto);

            return Ok();
        }

    }
}
