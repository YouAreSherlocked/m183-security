using m183_shovel_knight_security.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Data.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post Create(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Post GetById(Guid id)
        {
            return _context.Posts.Find(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public void Delete(Guid id)
        {
            var post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
