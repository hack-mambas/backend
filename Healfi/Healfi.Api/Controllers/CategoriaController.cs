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
    [Route("api/v1/categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriasService _service;
        private readonly ProdutosService _produtosService;

        public CategoriaController(CategoriasService service, ProdutosService produtosService)
        {
            _service = service;
            _produtosService = produtosService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoriaViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromBody] CreateCategoriaCommand command)
        {
            var (success, result) = await _service.Create(command);

            if (!success)
            {
                return BadRequest(new FailViewModel(result as string));
            }

            return Ok(result as CategoriaViewModel);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CategoriaViewModel), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> Cadastrar([FromRoute] Guid id, [FromBody] UpdateCategoriaCommand command)
        {
            var (success, result) = await _service.Update(command.AtribuirId(id));

            if (!success)
            {
                return BadRequest(new FailViewModel(result as string));
            }

            return Ok(result as CategoriaViewModel);
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(CategoriaViewModel), 200)]
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
        [ProducesResponseType(typeof(CategoriaViewModel), 200)]
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
        [ProducesResponseType(typeof(List<CategoriaViewModel>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterCategorias([FromQuery] Guid? produtorId, [FromQuery] string search)
        {
            var reg = await _service.ObterTodas(search, produtorId);

            return Ok(reg);
        }

        [HttpGet("{id:guid}/produtos")]
        [ProducesResponseType(typeof(List<ProdutoViewModel>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterProdutos([FromRoute] Guid categoriaId, [FromQuery] string search)
        {
            var reg = await _produtosService.ObterTodas(search, categoriaId, null);

            return Ok(reg);
        }
    }
}
