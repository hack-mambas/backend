using System.Threading.Tasks;
using Healfi.Api.Application.Commands;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/consumidor")]
    public class ConsumidorController : ControllerBase
    {
        public ConsumidorController()
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromBody] CreateConsumidorCommand command, [FromServices] AuthService authService)
        {
            var (success, message, result) = await authService.Register(command);

            if (!success)
            {
                return BadRequest(new FailViewModel(message));
            }

            return Ok(result);
        }
    }
}