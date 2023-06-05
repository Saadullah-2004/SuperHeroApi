using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperHeroApi.ApiKeyAttributes;
using SuperHeroApi.Models;
using SuperHeroApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _config;
        public LoginController(ILoginService config)
        {
            _config = config;
        }

        [ApiKey]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var token = _config.Login(userLogin);
                if (token != null)
                {
                    return Ok(token);
                }
                return NotFound("user not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
