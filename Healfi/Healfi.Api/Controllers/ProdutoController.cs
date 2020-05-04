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
    [Route("api/v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutosService _service;

        public ProdutoController(ProdutosService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProdutoViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromBody] CreateProdutoCommand command)
        {
            var (success, result) = await _service.Create(command);

            if (!success)
            {
                return BadRequest(new FailViewModel(result as string));
            }

            return Ok(result as ProdutoViewModel);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProdutoViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromRoute] Guid id, [FromBody] UpdateProdutoCommand command)
        {
            var (success, result) = await _service.Update(command.AtribuirId(id));

            if (!success)
            {
                return BadRequest(new FailViewModel(result as string));
            }

            return Ok(result as ProdutoViewModel);
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ProdutoViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var (success, result) = await _service.Delete(id);

            if (!success)
            {
                return BadRequest(new FailViewModel(result as string));
            }

            return NoContent();
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProdutoViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var reg = await _service.ObterPorIdView(id);

            if (reg == null)
            {
                return NotFound();
            }

            return Ok(reg);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutoViewModel>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterProdutos([FromQuery] Guid? produtorId, [FromQuery] Guid? categoriaId, [FromQuery] string search)
        {
            var reg = await _service.ObterTodas(search, categoriaId, produtorId);

            return Ok(reg);
        }
    }
}
