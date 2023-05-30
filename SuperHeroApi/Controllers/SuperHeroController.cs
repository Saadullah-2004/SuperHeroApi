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
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(repo.GetHeroList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var result = repo.GetHeroDetailsById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddHero(SuperHero hero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(repo.InsertHero(hero));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }    
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHero(SuperHero request)
        {
            try
            {
                var result = repo.ChangeHero(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = repo.DeleteHero(Id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            } 

        }
    }
}
