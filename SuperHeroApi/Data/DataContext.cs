using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Models;


namespace SuperHeroApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }

        public IQueryable<SuperHero> GetSuperHeroesWithCountryAndGenre()
        {
            // Joining the SuperHeroes and Countries tables based on the CountryId foreign key relationship
            return SuperHeroes
                .Join(Countries, sh => sh.CountryId, c => c.CountryId, (sh, c) => new { SuperHero = sh, Country = c })

                // Joining the result from the previous join with the Genres table based on the GenreId foreign key relationship
                .Join(Genres, sc => sc.SuperHero.GenreId, g => g.GenreId, (sc, g) => new { sc.SuperHero, sc.Country, Genre = g })

                // Selecting the desired properties from the join result and projecting them into a new SuperHero object
                .Select(x => new SuperHero
                {
                    Id = x.SuperHero.Id,
                    Name = x.SuperHero.Name,
                    FirstName = x.SuperHero.FirstName,
                    LastName = x.SuperHero.LastName,
                    Place = x.SuperHero.Place,
                    CountryId = x.SuperHero.CountryId,
                    Country = x.Country,
                    GenreId = x.SuperHero.GenreId,
                    Genre = x.Genre
                });
        }


    }
}
