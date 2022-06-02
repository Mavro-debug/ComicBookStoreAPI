using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookStoreAPI.Controllers
{
    [Route("comicBook/{comicBookId}/rating")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingManager _ratingManager;
        public RatingController(IRatingManager ratingManager)
        {
            _ratingManager = ratingManager;
        }
        [HttpGet]
        public IActionResult GetAll([FromRoute] int comicBookId)
        {
            var rating = _ratingManager.GetAll(comicBookId);

            return Ok(rating);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int comicBookId, [FromRoute] int id)
        {
            var rating = _ratingManager.GetById(comicBookId, id);

            return Ok(rating);
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int comicBookId, [FromBody] CreateRatingDto ratingDto)
        {
            int createdRatingId = await _ratingManager.Create(comicBookId, ratingDto);

            return Created($"/comicBook/{comicBookId}/rating/{createdRatingId}", null);
        }

        [Authorize(Roles = "Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Change([FromRoute] int comicBookId, [FromRoute] int id, [FromBody] CreateRatingDto ratingDto)
        {

            int resoult = await _ratingManager.Change(comicBookId, id, ratingDto);

            return Ok($"/comicBook/{comicBookId}/rating/{resoult}");
        }

        [Authorize(Roles = "Client,Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int comicBookId, [FromRoute] int id)
        {

            await _ratingManager.Delete(comicBookId, id);

            return NoContent();
        }
    }
}
