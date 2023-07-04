using SuperHeroApi.Models;
using SuperHeroApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
                _context.SaveChanges();
                match = true;
            }
            return match;
        }

        public SuperHero GetHeroDetailsById(int heroId)
        {
            return _context.SuperHeroes.Find(heroId);
        }

        public List<SuperHero> GetHeroList()
        {

            return _context.SuperHeroes
                .Include(sh => sh.Country) // Eager loading of Country
                .Include(sh => sh.Genre) // Eager loading of Genre
                .ToList();
        }

        public SuperHero InsertHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            _context.SaveChanges();
            return hero;
        }

        public SuperHero UpdateHero(SuperHero hero)
        {
            var existingHero = _context.SuperHeroes.Find(hero.Id);
            if (existingHero != null)
            {
                existingHero.FirstName = hero.FirstName;
                existingHero.LastName = hero.LastName;
                existingHero.Place = hero.Place;
                existingHero.Name = hero.Name;
                existingHero.CountryId = hero.CountryId;
                existingHero.GenreId = hero.GenreId;
                existingHero.Country = hero.Country;
                _context.SaveChanges();
                return existingHero;
            }
            return null;
        }
    }
}
