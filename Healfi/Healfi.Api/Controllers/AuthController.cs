using System.Linq;
using Healfi.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private HealfiContext _context;
        public AuthController(HealfiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(_context.Users.ToListAsync().GetAwaiter().GetResult());
        }
        
        [HttpGet("oi")]
        public IActionResult Test1()
        {
            return Ok("oiiii");
        }
    }
}