using System.Collections.Generic;
using System.Threading.Tasks;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Healfi.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cidade")]
    public class CidadeController : ControllerBase
    {
        private readonly CidadesService _service;
        
        public CidadeController(CidadesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Cidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterTodas([FromQuery] string search)
        {
            var cidades = await _service.ObterTodas(search);

            return Ok(cidades);
        }
        
        [HttpGet("por-estado/{sigla}")]
        [ProducesResponseType(typeof(List<Cidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterPorEstado(string sigla)
        {
            var cidades = await _service.ObterCidadesPorEstadoSigla(sigla);

            return Ok(cidades);
        }
        
        [HttpGet("estados")]
        [ProducesResponseType(typeof(List<Cidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterEstados()
        {
            var retorno = await _service.ObterEstados();

            return Ok(retorno);
        }
    }
}