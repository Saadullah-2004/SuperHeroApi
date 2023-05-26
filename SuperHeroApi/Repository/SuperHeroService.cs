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
 
        public SuperHero DeleteHero(int superheroId)
        {
            var hero = GetHeroDetailsById(superheroId);
            if (hero != null)
            {
                _context.SuperHeroes.Remove(hero);
                _context.SaveChanges();
            }
            return hero;
        }

        public SuperHero GetHeroDetailsById(int empId)
        {
            var hero = _context.SuperHeroes.Find(empId);
            return hero;
        }

        public List<SuperHero> GetHeroList()
        {
            return _context.SuperHeroes.ToList();
        }

        public int InsertHero(SuperHero Hero)
        {
            _context.SuperHeroes.Add(Hero);

            return _context.SaveChanges();
        }

        public SuperHero ChangeHero(SuperHero request)
        {
            var hero = _context.SuperHeroes.Find(request.Id);
            if (hero != null)
            {
                hero.FirstName = request.FirstName;
                hero.LastName = request.LastName;
                hero.Place = request.Place;
                hero.Name = request.Name;
            }
            _context.SaveChanges();
            return hero;
        }
    }
}
