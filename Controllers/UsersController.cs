using m183_shovel_knight_security.Data.Models;
using m183_shovel_knight_security.Data.Services;
using m183_shovel_knight_security.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class UsersController : AuthControllerBase
    {
        private UserService _userService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
              ILogger<UsersController> logger,
              UserService userService,
              IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticationModel model)
        {
            var user = _userService.Authenticate(model.Nickname, model.Password);

            if (user == null)
                return BadRequest(new { message = "Nickname or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var userInfo = new
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Token = tokenString
            };

            _logger.LogInformation($"User has logged in: {userInfo}");
            return Ok(userInfo);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] AuthenticationModel model)
        {
            try
            {
                var user = new User();
                user.Nickname = model.Nickname;
                var createdUser = _userService.Create(user , model.Password);
                _logger.LogInformation($"A new user was created: {createdUser}");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null) return BadRequest($"User with id {id} not found");
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

    }
}
