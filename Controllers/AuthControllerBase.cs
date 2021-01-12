using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace m183_shovel_knight_security.Controllers
{
    public class AuthControllerBase : ControllerBase
    {

        [NonAction]
        protected Guid GetUserIdFromToken()
        {
            //Method to get Id from authenticated user.
            var principal = HttpContext.User;
            if (principal?.Claims != null)
            {
                foreach (var claim in principal.Claims)
                {
                   // Console.WriteLine($"CLAIM TYPE: {claim.Type}; CLAIM VALUE: {claim.Value}");
                }
            }
            Guid userId = Guid.Parse(principal?.Claims?.SingleOrDefault(p => p.Type == ClaimTypes.Name)?.Value.ToUpper());
            return userId;
        }
    }
}
