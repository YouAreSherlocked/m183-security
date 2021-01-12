using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Data.Models
{
    public enum RoleName {
        User = 1,
        Admin = 2
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
