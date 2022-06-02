using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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
            var comicBookDtos = _comicBooksManager.GetAll(searchedPhrase);

            return Ok(comicBookDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var comicBookDtos = _comicBooksManager.GetById(id);

            return Ok(comicBookDtos);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromBody] NewComicBookDto newComicBookDto)
        {
            int newComicBookId = _comicBooksManager.CreateComicBook(newComicBookDto);

            return Ok(newComicBookId);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete([FromRoute] int id)
        {
            _comicBooksManager.RemoveComicBook(id);

            return NotFound();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Change([FromRoute] int id, [FromBody] NewComicBookDto newComicBookDto)
        {
            _comicBooksManager.UpdateComicBook(id, newComicBookDto);

            return Ok();
        }

    }
}
