using SuperHeroApi.Models;

namespace SuperHeroApi.Repository
{
    public class SuperHeroService : ISuperHeroService
    {
        private DataContext _context;

        public SuperHeroService(DataContext context)
        {
            _context = context;
        }
 
        public bool DeleteHero(int superheroId)
        {
            bool match = false;
            var hero = _context.SuperHeroes.Find(superheroId);
            if (hero != null)
            {
                _context.SuperHeroes.Remove(hero);
                _context.SaveChangesAsync();
                match = true;
            }
            return match;
        }

        public bool GetHeroDetailsById(int empId)
        {
            bool match = false;
            var hero = _context.SuperHeroes.Find(empId);
            if (hero != null)
            {
                match = true;
            }
            return match;
        }

        public List<SuperHero> GetHeroList()
        {
            return _context.SuperHeroes.ToList();
        }

        public SuperHero InsertHero(SuperHero Hero)
        {
            _context.SuperHeroes.AddAsync(Hero);

            _context.SaveChangesAsync();
            return Hero;
        }

        public SuperHero ChangeHero(SuperHero request)
        {
            var hero = _context.SuperHeroes.Where(x => x.Id == request.Id).FirstOrDefault();
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
            hero.Name = request.Name;
            _context.SaveChanges();
            return hero;
        }

    }
}
