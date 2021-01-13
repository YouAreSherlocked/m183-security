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

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <remarks>
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticationModel model)
        {
            var user = _userService.Authenticate(model.Nickname, model.Password);

            if (user == null)
            {
                _logger.LogWarning($"Failed login request for nickname: {model.Nickname} | password: {model.Password}");
                return BadRequest(new { message = "Nickname or password is/are incorrect" });
            }
                

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <remarks>
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegistrationModel model)
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

        /// <summary>
        /// Get a user by id (Admin role is needed)
        /// </summary>
        /// <remarks>
        /// Sensitive data (password) are in safety. :)
        /// </remarks>
        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(GetUserIdFromToken());
            if (user.RoleId != (int)RoleName.Admin)
            {
                return Unauthorized($"Access denied.");
            }
            var selectedUser = _userService.GetById(id);
            if (user == null) return BadRequest($"User with id {id} not found");
            return Ok(selectedUser);
        }

        /// <summary>
        /// Get all users (Admin role is needed)
        /// </summary>
        /// <remarks>
        /// Sensitive data (password) are in safety. :)
        /// </remarks>
        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _userService.GetById(GetUserIdFromToken());
            if (user.RoleId != (int)RoleName.Admin)
            {
                return Unauthorized($"Access denied.");
            }
            var users = _userService.GetAll();
            return Ok(users);
        }

    }
}
