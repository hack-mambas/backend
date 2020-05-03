using Healfi.Api.Application.Commands;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/produtor")]
    public class ProdutorController : ControllerBase
    {
        private readonly ProdutoresService _service;

        public ProdutorController(ProdutoresService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromBody] CreateProdutorCommand command, [FromServices] AuthService authService)
        {
            var (success, message, result) = await authService.Register(command);

            if (!success)
            {
                return BadRequest(new FailViewModel(message));
            }

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProdutorViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromRoute] Guid id, [FromBody] AtualizarProdutorCommand command)
        {
            var res = await _service.AtualizarProdutor(command.AtribuirId(id));

            if (res == null)
            {
                return BadRequest(new FailViewModel("Não foi possível atualizar"));
            }

            return Ok(res);
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProdutorViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var reg = await _service.ObterPorId(id);

            if (reg == null)
            {
                return NotFound();
            }

            return Ok(reg);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutorViewModel>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterProdutores([FromQuery] Guid? categoriaId, [FromQuery] Guid? cidadeId, [FromQuery] string search)
        {
            var reg = await _service.ObterTodas(cidadeId, categoriaId, search);

            return Ok(reg);
        }
    }
}
