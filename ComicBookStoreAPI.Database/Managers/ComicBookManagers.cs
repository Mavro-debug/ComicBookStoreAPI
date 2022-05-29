using AutoMapper;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Database.Managers
{
    public class ComicBookManagers
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ComicBookManagers(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
                Screenwriter = _screenwriterRepo.GetOrCreate(new Screenwriter() { Name = newComicBook.Screenriter }),
                Translator = _translatorRepo.GetOrCreate(new Translator() { Name = newComicBook.Translator }),
                Series = _seriesRepo.GetOrCreate(new Series() { Name = newComicBook.Series }),
                CoverType = _coverTypeRepo.GetOrCreate(new CoverType() { Name = newComicBook.CoverType }),

            };

            newComicBookEntity.ComicBookIllustrators = _comicBookIllustratorRepo.AssignRange(newComicBookEntity, newIllustratorsRange);
            newComicBookEntity.ComicBookHeroesTeams = _comicBookHeroesTeamsRepo.AssignRange(newComicBookEntity, newHerosTeamsRange);

            _comicBookRepository.Create(newComicBookEntity);


            return newComicBookEntity.Id;

        }

        public void RemoveComicBook(int id)
        {
            var comicBook = _comicBookRepository.GetById(id);

            _comicBookRepository.Delete(comicBook);

        }

        public void UpdateComicBook(int comicBookId, NewComicBookDto newComicBookDto)
        {
            var comicBook = _comicBookRepository.GetById(comicBookId);

            if (comicBook == null)
            {
                throw new NotFoundException($"The ComicBookEntityy with id = {comicBookId} could not be found");
            }

            comicBook.Title = newComicBookDto.Tytle;
            comicBook.Price = newComicBookDto.Price;
            comicBook.Edition = newComicBookDto.Edytion;
            comicBook.ReleaseDate = _mapper.Map<DateTime>(newComicBookDto.ReleaseDate);
            comicBook.NumberOfPages = newComicBookDto.NumberOfPages;
            comicBook.Screenwriter = _screenwriterRepo.GetOrCreate(new Screenwriter() { Name = newComicBookDto.Screenriter });
            comicBook.Translator = _translatorRepo.GetOrCreate(new Translator() { Name = newComicBookDto.Translator });
            comicBook.Series = _seriesRepo.GetOrCreate(new Series() { Name = newComicBookDto.Series });
            comicBook.CoverType = _coverTypeRepo.GetOrCreate(new CoverType() { Name = newComicBookDto.CoverType });


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


            //_comicBookRepository.Update(comicBook);


        }
    }
}
