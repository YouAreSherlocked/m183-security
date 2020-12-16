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
    public class PostsController : AuthControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create()
        {
            var userId = GetUserIdFromToken();
            //TODO createPost with SqlRaw
            return Ok(userId);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Guid id)
        {
            //Todo create delete with SqlRaw
            return Ok();
        }
    }
}
