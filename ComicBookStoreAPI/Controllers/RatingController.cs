using ComicBookStoreAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookStoreAPI.Controllers
{
    [Route("comicBook/{comicBookId}/rating")]
    public class RatingController : ControllerBase
    {
        private readonly IComicBookRatingBookService _ratingService;
        public RatingController(IComicBookRatingBookService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll([FromRoute] int comicBookId)
        {
            var rating = _ratingService.GetAll(comicBookId);

            return Ok(rating);
        }

        [HttpGet]
        [Route("getAll/{id}")]
        public IActionResult Get([FromRoute] int comicBookId, [FromRoute] int id)
        {
            var rating = _ratingService.GetById(comicBookId, id);

            return Ok(rating);
        }

    }
}
