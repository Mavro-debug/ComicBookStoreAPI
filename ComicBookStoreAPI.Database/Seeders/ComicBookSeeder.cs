using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;


namespace ComicBookStoreAPI.Database.Seeders
{
    public class ComicBookSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IComicBookManager _comicBooksManager;
        public ComicBookSeeder(ApplicationDbContext dbContext, IComicBookManager comicBooksManager)
        {
            this._dbContext = dbContext;
            _comicBooksManager = comicBooksManager;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.ComicBooks.Any())
                {
                    NewComicBookDto newComicBook = new NewComicBookDto()
                    {
                        Tytle = "Flash. Era Flasha. Tom 14",
                        Price = 38.49m,
                        Edytion = "2022",
                        ReleaseDate = new DateDto()
                        {
                            Year = 2022,
                            Month = 1,
                            Day = 19
                        },
                        NumberOfPages = 160,
                        Screenriter = "Joshua Williamson",
                        Translator = "Tomasz Kłoszewski",
                        Series = "DC Odrodzenie",
                        CoverType = "miękka ze skrzydełkami",
                        Description = "Kolejna odsłona przygód Szkarłatnego Sprintera w ramach linii wydawniczej DC Odrodzenie. Pojawienie się nikczemnego i niewiarygodnie potężnego Paradoksa uświadamia Flashowi, jaką cenę muszą płacić zwykli ludzie za wojny superbohaterów. W tej zapierającej dech w piersiach historii Barry Allen musi pędzić co sił, by uratować siebie, ukochanych bliskich, a nawet cały świat. Kiedy Paradoks wysyła do walki sprzymierzonego z nim Godspeeda, Flash zmuszony jest zawrzeć sojusz ze swoim największym wrogiem, Odwrotnym Flashem. Gdy całe dziedzictwo Flasha jest zagrożone, Barry zdaje sobie sprawę, że jedynym ratunkiem dla multiwersum może okazać się człowiek, który zabił jego matkę. Wszystko to prowadzi Barry'ego Allena do wstrząsającego finału, który zwiastuje pojawienie się niebezpieczeństwa, jakiego nawet sobie nie wyobrażał.",
                        Illustrators = new List<string>() { "Howard Porter", "Rafa Sandoval", "Jordi Tarragona" },
                        HeroesTeams = new List<string>() { "The Flash" }
                    };

                    _comicBooksManager.CreateComicBook(newComicBook);

                    var comic = _dbContext.ComicBooks.Where(x => x.Title == "Flash. Era Flasha. Tom 14").FirstOrDefault();
                    comic.Posters = new List<Poster>()
                    {
                        new Poster()
                        {
                            Cover = true,
                            Path = "Flash.EraFlasha.Tom14/poster1.jpg"
                        }
                    };

                    NewComicBookDto newComicBook2 = new NewComicBookDto()
                    {
                        Tytle = "Superman – Prawda ujawniona. Tom 3",
                        Price = 34.99m,
                        Edytion = "2021",
                        ReleaseDate = new DateDto()
                        {
                            Year = 2021,
                            Month = 1,
                            Day = 20
                        },
                        NumberOfPages = 192,
                        Screenriter = "Brian Michael Bendis",
                        Translator = "Jakub Syty",
                        Series = "UNIWERSUM DC",
                        CoverType = "miękka ze skrzydełkami",
                        Description = "W trzecim tomie serii „Superman” wielokrotnie wyróżniony Nagrodą Eisnera scenarzysta Brian Michael Bendis („Jessica Jones”, „New Avengers”) oraz rysownicy Ivan Reis („Liga Sprawiedliwości”, „Aquaman”), Kevin Maguire („Justice League International”) oraz wielu innych łączą siły, by ukazać symboliczne ujawnienie sekretnej tożsamości Supermana. Superman od zawsze prowadził podwójne życie. Jako dobrze ułożony Clark Kent może być tylko zwykłym obywatelem pracującym za biurkiem, mającym przyjaciół i zwyczajne problemy. Lecz jak może twierdzić, że stoi na straży prawdy, skoro okłamuje niemal wszystkich na swój temat? Clark jest wreszcie gotów ujawnić swoją sekretną tożsamość, ale zanim powie o niej światu, musi skonsultować się z najbliższymi: żoną, kuzynką, najlepszym kumplem, a nawet ze swoim szefem. A gdy już świat (w tym wrogowie) pozna prawdę o Supermanie, czy życie Clarka Kenta będzie mogło być takie samo? Poza tym synowie Supermana i Batmana znów zaczynają działać razem, lecz Damian Wayne nie może uwierzyć, jak bardzo Jon Kent się zmienił po powrocie z międzygalaktycznej podróży ze swoim dziadkiem Jor-Elem! ",
                        Illustrators = new List<string>() { "Joe Prador", "David Lafuente", "Kevin Maguire", "Ivan Reis" },
                        HeroesTeams = new List<string>() { "Superman" }
                    };

                    _comicBooksManager.CreateComicBook(newComicBook2);

                    var comic2 = _dbContext.ComicBooks.Where(x => x.Title == "Superman – Prawda ujawniona. Tom 3").FirstOrDefault();
                    comic2.Posters = new List<Poster>()
                    {
                        new Poster()
                        {
                            Cover = true,
                            Path = "Superman–Prawdaujawniona.Tom3/poster1.jpg"
                        }
                    };

                    _dbContext.SaveChanges();
                }
            }
        }


    }
}
