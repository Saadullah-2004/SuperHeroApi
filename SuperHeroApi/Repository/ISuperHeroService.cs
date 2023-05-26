using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Models;
using System;

namespace SuperHeroApi.Repository
{

    public interface ISuperHeroService
    {

        public List<SuperHero> GetHeroList();

        public SuperHero GetHeroDetailsById(int empId);

        public int InsertHero(SuperHero Hero);

        public SuperHero ChangeHero(SuperHero request);

        public SuperHero DeleteHero(int superheroId);


    }
}
