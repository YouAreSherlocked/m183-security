using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Role Role { get; set; }
    }
}
