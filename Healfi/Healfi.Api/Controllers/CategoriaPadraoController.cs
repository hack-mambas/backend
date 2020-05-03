using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Healfi.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/categoria-padrao")]
    public class CategoriaPadraoController : ControllerBase
    {
        private readonly CategoriaPadraosService _service;
        
        public CategoriaPadraoController(CategoriaPadraosService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaPadrao>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterTodas([FromQuery] string search)
        {
            var cidades = await _service.ObterCategoriasPadrao(search);

            return Ok(cidades);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoriaPadrao), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _service.ObterPorId(id);

            return Ok(retorno);
        }
    }
}