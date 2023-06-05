using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using SuperHeroApi.Models;
using SuperHeroApi.Repository;
using System.Data;
using System.Security.Claims;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService repo { get; set; }
        public UserController(IUserService _repo)
        {
            repo = _repo;
        }

        //For admin Only
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            try
            {
                var currentUser = repo.GetUser();
                return Ok($"Hi you are an {currentUser.Role}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
