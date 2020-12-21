using System.Runtime.CompilerServices;
using m183_shovel_knight_security.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace m183_shovel_knight_security.Data.Services
{
    //This service uses no ORM properties for database communication. Just SQL.
    public class PostService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PostService> _logger;

        public PostService(ApplicationDbContext context, ILogger<PostService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Post GetById(Guid id)
        {
            string query = "SELECT * FROM Posts WHERE id = {0}";
            _logger.LogInformation($"Executing query: {query} | params: {id}");
            var post = _context.Posts.FromSqlRaw(query, id).FirstOrDefault();
            return post;
        }

        public IEnumerable<Post> GetByUserId(Guid userId)
        {
            string query = "SELECT * FROM Posts WHERE userId = {0}";
            _logger.LogInformation($"Executing query: {query} | params: {userId}");
            var posts = _context.Posts.FromSqlRaw(query, userId).OrderByDescending(x => x.CreatedAt).ToList();
            return posts;
        }

        public IEnumerable<Post> GetAll()
        {
            string query = "SELECT * FROM Posts";
            _logger.LogInformation($"Executing query: {query}");
            var posts = _context.Posts.FromSqlRaw(query).OrderByDescending(x => x.CreatedAt).ToList();
            return posts;
        }

        public Post Create(Post post)
        {
            FormattableString query = $@"INSERT INTO Posts(Id, Text, ImageUrl, UserId, CreatedAt, UpdatedAt)
                                      VALUES({post.Id}, { post.Text}, { post.ImageUrl}, { post.UserId}, { post.CreatedAt}, { post.UpdatedAt})";
            _logger.LogInformation($"Executing query: {query}");
            bool result = Convert.ToBoolean(_context.Database.ExecuteSqlInterpolated(query));
            if (result)
            {
                var createdPost = GetById(post.Id);
                return createdPost;
            }
            _logger.LogCritical($"Something went wrong | query: {query}");
            return default;
        }

        public Post Update(Post post)
        {
            FormattableString query = $@"UPDATE Posts SET Text = {post.Text}, ImageUrl = {post.ImageUrl}, UpdatedAt = {post.UpdatedAt}
                                        WHERE id = {post.Id}";
            _logger.LogInformation($"Executing query: {query}");
            bool result = Convert.ToBoolean(_context.Database.ExecuteSqlInterpolated(query));
            if (result)
            {
                var updatedPost = GetById(post.Id);
                return updatedPost;
            }
            _logger.LogCritical($"Something went wrong | query: {query}");
            return default;
        }

        public bool Delete(Guid id)
        {
            FormattableString query = $"DELETE FROM Posts WHERE Id = {id};";
           _logger.LogInformation($"Executing query: {query}");
            bool result = Convert.ToBoolean(_context.Database.ExecuteSqlInterpolated(query));
            return result;
        }
    }
}
