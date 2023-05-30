using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Models;
using System;

namespace SuperHeroApi.Repository
{

    public interface ISuperHeroService
    {

        public List<SuperHero> GetHeroList();

        public bool GetHeroDetailsById(int empId);

        public SuperHero InsertHero(SuperHero Hero);

        public SuperHero ChangeHero(SuperHero request);

        public bool DeleteHero(int superheroId);


    }
}
