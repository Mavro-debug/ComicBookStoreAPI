using AutoMapper;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.DbContext;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Services
{
    public class RatingService : IRatingService
    {
        private readonly IEntityHelper _entityHelper;
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public RatingService(IEntityHelper entityHelper, IApplicationDbContext dbContext,
            IMapper mapper, IAuthorizationService authorizationService)
        {
            _entityHelper = entityHelper;
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }
        public List<RatingDto> GetAll(int comicBookId)
        {
            var comicBook = _dbContext.ComicBooks
                .Include(x => x.Ratings)
                .FirstOrDefault(c => c.Id == comicBookId);

            if (comicBook == null)
                throw new DatabaseException($"The ComicBook with Id: {comicBookId} was not found");

            var ratings = comicBook.Ratings.ToList();

            if (ratings == null)
                throw new DatabaseException($"Ratings for the ComicBook with Id: {comicBookId} were not found");

            var ratingDto = _mapper.Map<List<RatingDto>>(ratings);

            return ratingDto;
        }

        public RatingDto GetById(int comicBookId, int id )  
        {
            var comicBook = _dbContext.ComicBooks
               .Include(x => x.Ratings)
               .FirstOrDefault(c => c.Id == comicBookId);

            if (comicBook == null)
                throw new DatabaseException($"The ComicBook with Id: {comicBookId} was not found");

            var ratings = comicBook.Ratings.FirstOrDefault(r => r.Id == id);

            if (ratings == null)
                throw new DatabaseException($"Rating with the Id: {id} for the ComicBook with Id: {comicBookId} were not found");

            var ratingDto = _mapper.Map<RatingDto>(ratings);

            return ratingDto;
        }

        public int Create(int comicBookId, ApplicationUser user, RatingDto ratingDto)
        {
            Rating rating = _mapper.Map<Rating>(ratingDto);

            var comicBook = _dbContext.ComicBooks.FirstOrDefault(x => x.Id == comicBookId);

            if (comicBook == null)
            {
                throw new DatabaseException($"ComicBook entity with Id {comicBookId} could not be found");
            }

            rating.ComicBook = comicBook;

            rating.User = user;

            _ratingRepo.Create(rating);

            return rating.Id;
        }

        public void Change(int ratingId, RatingDto ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);

            //var authorizationResoult = _authorizationService.AuthorizeAsync()

            _ratingRepo.Update(ratingId, rating);
            
        }

    }
}
