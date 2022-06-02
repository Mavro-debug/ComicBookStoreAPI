using AutoMapper;
using ComicBookStoreAPI.Database;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Database.Managers
{
    public class RatingManager : IRatingManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public RatingManager( ApplicationDbContext dbContext,
            IMapper mapper, IAuthorizationService authorizationService)
        {
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

        public int Create(int comicBookId, ApplicationUser user, CreateRatingDto ratingDto)
        {
            Rating rating = _mapper.Map<Rating>(ratingDto);

            var comicBook = _dbContext.ComicBooks.FirstOrDefault(x => x.Id == comicBookId);

            if (comicBook == null)
            {
                throw new NotFoundException($"ComicBook entity with Id {comicBookId} could not be found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync()

            var ratingExists = comicBook.Ratings.Any(x => x.User.Id == user.Id);

            if (ratingExists)
            {
                throw new Exception($"Unauthorized, the user with Id: {user.Id} already created rating of this Comicbook Entity Id: {comicBookId}");
            }

            rating.ComicBook = comicBook;

            rating.User = user;

            _dbContext.Add(rating);

            return rating.Id;
        }

        public int Change(int comicBookId, ApplicationUser user, CreateRatingDto ratingDto)
        {
            var comicBook = _dbContext.ComicBooks.FirstOrDefault(x => x.Id == comicBookId);

            if (comicBook == null)
            {
                throw new DatabaseException($"ComicBook entity with Id {comicBookId} could not be found");
            }

            var rating = comicBook.Ratings.FirstOrDefault(x => x.User.Id == user.Id);

            if (rating == null)
            {
                throw new DatabaseException($"Rating entity with ComicBook Id: {comicBookId} and User Id: {user.Id} could not be found");
            }

            rating.Commentary = ratingDto.Commentary;

            rating.Grade = ratingDto.Grade;

            _dbContext.SaveChanges();

            return rating.Id;
        }

    }
}
