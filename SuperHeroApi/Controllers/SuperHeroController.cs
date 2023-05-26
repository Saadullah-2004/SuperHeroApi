using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.Models;
using SuperHeroApi.Repository;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private ISuperHeroService repo { get; set; }

        public SuperHeroController(ISuperHeroService _Repo)
        {
            repo = _Repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repo.GetHeroList());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var hero = repo.GetHeroDetailsById(Id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(hero);

        }

        [HttpPost]
        public IActionResult AddHero(SuperHero hero)
        {   
            repo.InsertHero(hero);
            return Ok(repo.GetHeroList());
        }

        [HttpPut]
        public IActionResult UpdateHero(SuperHero request)
        {
            var hero = repo.ChangeHero(request);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(repo.GetHeroList());
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var hero = repo.DeleteHero(Id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(repo.GetHeroList());

        }
    }
}
