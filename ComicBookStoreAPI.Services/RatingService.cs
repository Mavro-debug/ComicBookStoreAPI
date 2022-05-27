using AutoMapper;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.DbContext;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Services
{
    public class RatingService : IComicBookRatingBookService
    {
        private readonly IRepository<ComicBook> _comicBookRepository;
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RatingService(IRepository<ComicBook> comicBookRepository, IApplicationDbContext dbContext,
            IMapper mapper)
        {
            _comicBookRepository = comicBookRepository;
            _dbContext = dbContext;
            _mapper = mapper;
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
    }
}
