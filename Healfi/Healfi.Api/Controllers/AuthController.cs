using Healfi.Api.Application.Commands;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Healfi.Api.Data;
using Healfi.Api.Domain.Enumerators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly HealfiContext _context;
        private readonly ProdutoresService _produtoresService;
        private readonly ConsumidoresService _consumidoresService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(HealfiContext context, ProdutoresService produtoresService, ConsumidoresService consumidoresService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _produtoresService = produtoresService;
            _consumidoresService = consumidoresService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Login([FromBody] AuthenticateCommand command, [FromServices] AuthService authService)
        {
            var r = await authService
                .Authenticate(command.Username, command.Senha);

            if (!r.success)
            {
                return BadRequest(new FailViewModel(r.message));
            }

            return Ok(r.result);
        }

        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Me()
        {
            var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirstValue("Healfi.Claims.Id"));
            var userData = await _context.Users.Where(c => c.Id == userId).Select(x => new
            {
                x.Tipo,
                Id = x.Tipo == TipoVinculoEnum.Produtor ? x.Produtor.Id : x.Consumidor.Id
            }).SingleOrDefaultAsync();

            if (userData == null)
            {
                return NotFound();
            }

            var res = userData.Tipo == TipoVinculoEnum.Consumidor
                ? await _consumidoresService.ObterPorId(userData.Id)
                : (object)await _produtoresService.ObterPorId(userData.Id);

            return Ok(res);
        }
    }
}
