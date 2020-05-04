using Healfi.Api.Application.Commands;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healfi.Api.Domain;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/produtor")]
    public class ProdutorController : ControllerBase
    {
        private readonly ProdutoresService _service;
        private readonly CategoriasService _categoriaService;
        private readonly ProdutosService _produtoService;

        public ProdutorController(ProdutoresService service, CategoriasService categoriaService, ProdutosService produtoService)
        {
            _service = service;
            _categoriaService = categoriaService;
            _produtoService = produtoService;
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
        
        [HttpGet("{id:guid}/categorias")]
        [ProducesResponseType(typeof(List<CategoriaViewModel>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterCategorias([FromRoute] Guid produtorId, [FromQuery] string search)
        {
            var reg = await _categoriaService.ObterTodas(search, produtorId);

            return Ok(reg);
        }
        
        
        [HttpGet("{id:guid}/produtos")]
        [ProducesResponseType(typeof(List<ProdutoViewModel>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterProdutos(
            [FromRoute] Guid produtorId, 
            [FromQuery] Guid? categoriaId, 
            [FromQuery] string search
        )
        {
            var reg = await _produtoService.ObterTodas(search, categoriaId, produtorId);

            return Ok(reg);
        }
        
        [HttpGet("{id:guid}/cidades-atendidas")]
        [ProducesResponseType(typeof(List<Cidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterCidadesAtendidas(
            [FromRoute] Guid id,
            [FromQuery] string search
        )
        {
            var reg = await _service.ObterCidadesAtendidas(id, search);

            return Ok(reg);
        }
        
        [HttpPost("{id:guid}/cidades-atendidas/{cidadeId:guid}")]
        [ProducesResponseType(typeof(List<Cidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> CadastrarCidadeAtendida(
            [FromRoute] Guid id,
            [FromRoute] Guid cidadeId
        )
        {
            var reg = await _service.CadastrarCidadeAtendida(id, cidadeId);

            return Ok(reg);
        }
        
        [HttpGet("{id:guid}/especialidades-vinculadas")]
        [ProducesResponseType(typeof(List<Especialidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterEspecialidadesVinculadas(
            [FromRoute] Guid id,
            [FromQuery] string search
        )
        {
            var reg = await _service.ObterEspecialidadesVinculadas(id, search);

            return Ok(reg);
        }
        
        [HttpPost("{id:guid}/especialidades-vinculadas/{especialidadeId:guid}")]
        [ProducesResponseType(typeof(List<Especialidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> CadastrarEspecialidade(
            [FromRoute] Guid id,
            [FromRoute] Guid especialidadeId
        )
        {
            var reg = await _service.CadastrarEspecialidadesVinculadas(id, especialidadeId);

            return Ok(reg);
        }
        
        [HttpGet("{id:guid}/formas-entrega-atendidas")]
        [ProducesResponseType(typeof(List<FormaEntrega>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterFormasEntregaVinculadas(
            [FromRoute] Guid id,
            [FromQuery] string search
        )
        {
            var reg = await _service.ObterFormasEntregaVinculadas(id, search);

            return Ok(reg);
        }
        
        [HttpPost("{id:guid}/formas-entrega-atendidas/{formaEntregaId:guid}")]
        [ProducesResponseType(typeof(List<FormaEntrega>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> CadastrarFormaEntrega(
            [FromRoute] Guid id,
            [FromRoute] Guid formaEntregaId,
            [FromBody] string observacoes
        )
        {
            var reg = await _service.CadastrarFormasEntregaVinculadas(id, formaEntregaId, observacoes);

            return Ok(reg);
        }
    }
}
