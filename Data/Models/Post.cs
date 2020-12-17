using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User User { get; set; }
    }
}
