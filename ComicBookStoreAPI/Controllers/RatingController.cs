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
        private readonly UserManager<ApplicationUser> _userManager;
        public RatingController(IRatingManager ratingManager, UserManager<ApplicationUser> userManager)
        {
            _ratingManager = ratingManager;
            _userManager = userManager;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int comicBookId, [FromBody] RatingDto ratingDto)
        {
            var user = await _userManager.GetUserAsync(User);

            int createdRatingId = _ratingManager.Create(comicBookId, user, ratingDto);

            return Created($"/comicBook/{comicBookId}/rating/{createdRatingId}", null);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Change([FromRoute] int id, [FromBody] RatingDto ratingDto)
        {
            _ratingManager.Change(id, ratingDto);

            return Ok();
        }
    }
}
