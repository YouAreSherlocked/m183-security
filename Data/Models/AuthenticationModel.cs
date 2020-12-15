using System.ComponentModel.DataAnnotations;

namespace m183_shovel_knight_security.Data.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
