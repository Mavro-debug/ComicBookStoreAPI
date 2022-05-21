using ComicBookStoreAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookStoreAPI.Controllers
{
    [Route("comicBookRating/{comicBookId}")]
    public class ComicBookRatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public ComicBookRatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        public IActionResult GetAll([FromRoute] int comicBookId)
        {
            var rating = _ratingService.GetAllWithComicBookId(comicBookId);

            return Ok(rating);
        }
    }
}
