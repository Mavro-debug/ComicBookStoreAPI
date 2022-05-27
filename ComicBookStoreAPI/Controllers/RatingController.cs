using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicBookStoreAPI.Controllers
{
    [Route("comicBook/{comicBookId}/rating")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet]
        public IActionResult GetAll([FromRoute] int comicBookId)
        {
            var rating = _ratingService.GetAll(comicBookId);

            return Ok(rating);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int comicBookId, [FromRoute] int id)
        {
            var rating = _ratingService.GetById(comicBookId, id);

            return Ok(rating);
        }


        [HttpPost]
        public IActionResult Create([FromRoute] int comicBookId, [FromBody] RatingDto ratingDto)
        {

        }
    }
}
