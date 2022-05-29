using AutoMapper;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace ComicBookStoreAPI.Database.Managers
{
    public class ComicBookManager : IComicBookManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEntityHelper _entityHelper;
        private readonly IRepository<ComicBookIllustrator, ComicBook, Illustrator> _comicBookIllustratorRepo;
        private readonly IRepository<ComicBookHeroesTeams, ComicBook, HeroesTeams> _comicBookHeroesTeamsRepo;
        public ComicBookManager(ApplicationDbContext dbContext, IMapper mapper,
            IEntityHelper entityHelper,
            IRepository<ComicBookIllustrator, ComicBook, Illustrator> comicBookIllustratorRepo,
            IRepository<ComicBookHeroesTeams, ComicBook, HeroesTeams> comicBookHeroesTeamsRepo)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _entityHelper = entityHelper;
            _comicBookIllustratorRepo = comicBookIllustratorRepo;
            _comicBookHeroesTeamsRepo = comicBookHeroesTeamsRepo;
        }


        public IEnumerable<ComicBookCardDto> GetAllCards()
        {
            var comicBooks = _dbContext
                .ComicBooks
                .Include(r => r.Posters)
                .ToList();

            var comicBookCards = _mapper.Map<List<ComicBookCardDto>>(comicBooks);

            return comicBookCards;
        }

        public IEnumerable<ComicBookCardDto> GetAllSearchedCards(string searchedPhrase)
        {

            if (!string.IsNullOrEmpty(searchedPhrase))
            {
                var comicBooks = _dbContext
                .ComicBooks
                .Include(r => r.Posters)
                .Where(c => c.Title.ToLower().Contains(searchedPhrase.ToLower()));

                if (!comicBooks.Any())
                {
                    comicBooks = _dbContext.ComicBooks
                        .Include(r => r.Posters)
                        .Where(c => c.Description.ToLower().Contains(searchedPhrase.ToLower()));
                }

                if (!comicBooks.Any())
                {
                    comicBooks = _dbContext.ComicBooks
                        .Include(r => r.Posters)
                        .Where(c => c.Translator.Name.ToLower().Contains(searchedPhrase.ToLower()));
                }

                if (!comicBooks.Any())
                {
                    comicBooks = _dbContext.ComicBooks
                        .Include(r => r.Posters)
                        .Where(c => c.Series.Name.ToLower().Contains(searchedPhrase.ToLower()));
                }

                if (!comicBooks.Any())
                {
                    comicBooks = _dbContext.ComicBooks
                        .Include(r => r.Posters)
                        .Where(c => c.ComicBookHeroesTeams.Any(ch => ch.HeroesTeams.Name.ToLower().Contains(searchedPhrase.ToLower())));
                }

                if (!comicBooks.Any())
                {
                    comicBooks = _dbContext.ComicBooks
                        .Include(r => r.Posters)
                        .Where(c => c.ComicBookIllustrators.Any(ci => ci.Illustrator.Name.ToLower().Contains(searchedPhrase.ToLower())));
                }

                var comicBookCards = _mapper.Map<List<ComicBookCardDto>>(comicBooks);

                return comicBookCards;
            }
            else
            {
                return new List<ComicBookCardDto>();
            }



        }


        public int CreateComicBook(NewComicBookDto newComicBook)
        {
            var newIllustratorsRange = new List<Illustrator>();
            foreach (string illustratorName in newComicBook.Illustrators)
            {
                newIllustratorsRange.Add(new Illustrator() { Name = illustratorName });
            }

            var newHerosTeamsRange = new List<HeroesTeams>();
            foreach (string heroTeamName in newComicBook.HeroesTeams)
            {
                newHerosTeamsRange.Add(new HeroesTeams() { Name = heroTeamName });
            }


            var newComicBookEntity = new ComicBook()
            {
                Title = newComicBook.Tytle,
                Price = newComicBook.Price,
                Edition = newComicBook.Edytion,
                ReleaseDate = _mapper.Map<DateTime>(newComicBook.ReleaseDate),
                NumberOfPages = newComicBook.NumberOfPages,
                Discount = newComicBook.Discount,
                Description = newComicBook.Description,
                Screenwriter = _entityHelper.GetFirstOrDefaultAlike(new Screenwriter() { Name = newComicBook.Screenriter }) ?? 
                    new Screenwriter() { Name = newComicBook.Screenriter },

                Translator = _entityHelper.GetFirstOrDefaultAlike(new Translator() { Name = newComicBook.Translator }) ??
                    new Translator() { Name = newComicBook.Translator },

                Series = _entityHelper.GetFirstOrDefaultAlike(new Series() { Name = newComicBook.Series }) ??
                    new Series() { Name = newComicBook.Series },

                CoverType = _entityHelper.GetFirstOrDefaultAlike(new CoverType() { Name = newComicBook.CoverType }) ??
                    new CoverType() { Name = newComicBook.CoverType },

            };


            newComicBookEntity.ComicBookIllustrators = _comicBookIllustratorRepo.AssignRange(newComicBookEntity, newIllustratorsRange);
            newComicBookEntity.ComicBookHeroesTeams = _comicBookHeroesTeamsRepo.AssignRange(newComicBookEntity, newHerosTeamsRange);

            _dbContext.Add(newComicBookEntity);

            bool resoul = _dbContext.SaveChanges() > 0;

            if (resoul == false)
            {
                throw new DatabaseException($"New ComicBook entity was added but no changes occured");
            }

            return newComicBookEntity.Id;

        }

        public void RemoveComicBook(int id)
        {
            var comicBook = _dbContext.ComicBooks.FirstOrDefault(x => x.Id == id);

            if (comicBook == null)
            {
                throw new DatabaseException($"ComicBook entity with Id: {id} could not be found");
            }

            _dbContext.Remove(comicBook);

            bool resoul = _dbContext.SaveChanges() > 0;

            if (resoul == false)
            {
                throw new DatabaseException($"ComicBook entity was removed but no changes occured");
            }

        }

        public void UpdateComicBook(int comicBookId, NewComicBookDto newComicBookDto)
        {
            var comicBook = _dbContext.ComicBooks.FirstOrDefault(x => x.Id == comicBookId);

            if (comicBook == null)
            {
                throw new NotFoundException($"The ComicBookEntityy with id = {comicBookId} could not be found");
            }

            comicBook.Title = newComicBookDto.Tytle;
            comicBook.Price = newComicBookDto.Price;
            comicBook.Edition = newComicBookDto.Edytion;
            comicBook.ReleaseDate = _mapper.Map<DateTime>(newComicBookDto.ReleaseDate);
            comicBook.NumberOfPages = newComicBookDto.NumberOfPages;

            comicBook.Screenwriter = _entityHelper.GetFirstOrDefaultAlike(new Screenwriter() { Name = newComicBookDto.Screenriter }) ??
                    new Screenwriter() { Name = newComicBookDto.Screenriter };

            comicBook.Translator = _entityHelper.GetFirstOrDefaultAlike(new Translator() { Name = newComicBookDto.Translator }) ??
                    new Translator() { Name = newComicBookDto.Translator };

            comicBook.Series = _entityHelper.GetFirstOrDefaultAlike(new Series() { Name = newComicBookDto.Series }) ??
                    new Series() { Name = newComicBookDto.Series };

            comicBook.CoverType = _entityHelper.GetFirstOrDefaultAlike(new CoverType() { Name = newComicBookDto.CoverType }) ??
                    new CoverType() { Name = newComicBookDto.CoverType };


            var newIllustratorsRange = new List<Illustrator>();
            foreach (string illustratorName in newComicBookDto.Illustrators)
            {
                newIllustratorsRange.Add(new Illustrator() { Name = illustratorName });
            }


            comicBook.ComicBookIllustrators = _comicBookIllustratorRepo.AssignRange(comicBook, newIllustratorsRange);
            _comicBookIllustratorRepo.DeleteOutsideRange(newIllustratorsRange, _dbContext.ComicBooks.Where(c => c.Id == comicBookId).Single());


            var newHerosTeamsRange = new List<HeroesTeams>();
            foreach (string heroTeamName in newComicBookDto.HeroesTeams)
            {
                newHerosTeamsRange.Add(new HeroesTeams() { Name = heroTeamName });
            }

            comicBook.ComicBookHeroesTeams = _comicBookHeroesTeamsRepo.AssignRange(comicBook, newHerosTeamsRange);
            _comicBookHeroesTeamsRepo.DeleteOutsideRange(newHerosTeamsRange, _dbContext.ComicBooks.Where(c => c.Id == comicBookId).Single());


            _dbContext.SaveChanges();


        }
    }
}
