using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Data.Models
{
    public class PostDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Text { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
