using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoundtheCode.BasicAuthentication.Shared.Authentication.Basic.Attributes;
using SuperHeroApi.Models;
using SuperHeroApi.Repository;
using Microsoft.AspNetCore.Authorization;
using SuperHeroApi.ApiKeyAttributes;

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
        public IActionResult Get(int Id)
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
        public IActionResult AddHero([FromBody] SuperHero hero)
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
        public IActionResult UpdateHero(SuperHero request)
        {
            try
            {
                var result = repo.UpdateHero(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
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
