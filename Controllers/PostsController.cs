using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using m183_shovel_knight_security.Data.Models;
using m183_shovel_knight_security.Data.Services;
using m183_shovel_knight_security.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace m183_shovel_knight_security.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class PostsController : AuthControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly PostService _postService;
        private readonly UserService _userService;
        public PostsController(ILogger<PostsController> logger, PostService postService, UserService userService)
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;
        }

        /// <summary>
        /// Get a post by id.
        /// </summary>
        /// <remarks>
        /// Authentication needed.
        /// </remarks>
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation($"Getting Post {id}");
            var post = _postService.GetById(id);
            if (post == null)
            {
                _logger.LogWarning($"Get({id}) NOT FOUND");
                return BadRequest($"Post with id {id} not found");
            }
            return Ok(post);
        }

        /// <summary>
        /// Get all posts.
        /// </summary>
        /// <remarks>
        /// Authentication needed.
        /// </remarks>
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"Getting all Posts");
            return Ok(_postService.GetAll());
        }

         /// <summary>
        /// Get all posts by authenticated user.
        /// </summary>
        /// <remarks>
        /// Authentication needed.
        /// </remarks>
        [HttpGet]
        public IActionResult GetByUser()
        {
            Guid authUserId = GetUserIdFromToken();
            _logger.LogInformation($"Getting all Posts from user id {authUserId}");
            return Ok(_postService.GetByUserId(authUserId));
        }

        /// <summary>
        /// Creates a post. Needs authentication.
        /// </summary>
        /// <remarks>
        ///  Authentication needed.
        /// </remarks>
        [HttpPost]
        public IActionResult Create([FromBody] PostDTO postDTO)
        {
            var post = new Post()
            {
                Id = postDTO.Id,
                Text = postDTO.Text,
                ImageUrl = postDTO.ImageUrl,
                CreatedAt = postDTO.CreatedAt,
                UpdatedAt = postDTO.CreatedAt,
                UserId = GetUserIdFromToken()
            };
            _logger.LogInformation($"Creating Post {post}");
            post = _postService.Create(post);
            return Ok(post);
        }

        /// <summary>
        /// Allows a user to update his own post. Admins are able to edit any post.
        /// </summary>
        /// <remarks>
        /// Authentication needed.
        /// </remarks>
        [HttpPatch]
        public IActionResult Update(Guid id, [FromBody] PostDTO postDTO)
        {
            var user = _userService.GetById(GetUserIdFromToken());
            var post = _postService.GetById(id);

            if (post == null)
            {
                _logger.LogInformation($"Update({id}) NOT FOUND");
                return BadRequest($"Post with id {id} not found");
            }
            if (post.UserId != user.Id || user.RoleId != (int)RoleName.Admin){
                 _logger.LogWarning($"Update({id}) USER failing to update a post (not admin/not owner) - User id: {user.Id}");
                 return Unauthorized($"Access denied.");
            } else {
                post.Text = postDTO.Text;
                post.ImageUrl = postDTO.ImageUrl;
                post.UpdatedAt = postDTO.CreatedAt;
            }

            _logger.LogInformation($"Updating Post {post}");
            var updatedPost = _postService.Update(post);
            return Ok(updatedPost);
        }

        /// <summary>
        /// Delete a post. (Admin only)
        /// </summary>
        /// <remarks>
        /// Authentication needed.
        /// </remarks>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var user = _userService.GetById(GetUserIdFromToken());
            if (user.RoleId != (int)RoleName.Admin){
                 _logger.LogWarning($"Delete({id}) USER failing to delete a post (not admin) - User id: {user.Id}");
                 return Unauthorized($"Access denied.");
            }

            if (_postService.Delete(id))
            {
                _logger.LogInformation($"Deleting Post {id}");
                return Ok();
            }
            else
            {
                _logger.LogInformation($"Delete({id}) NOT FOUND");
                return BadRequest($"Post with id {id} doens't exists");
            }

        }
    }
}
