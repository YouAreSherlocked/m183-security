using m183_shovel_knight_security.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private ShellHelper _shellHelper;
        public FilesController(ILogger<FilesController> logger, ShellHelper shellHelper)
        {
            _logger = logger;
            _shellHelper = shellHelper;
        }

        [HttpGet]
        public IActionResult Command([Required] string cmd)
        {
            _logger.LogInformation($"User executed the following command: {cmd}");
            return Ok(_shellHelper.Bash(cmd));
        }
    }
}
