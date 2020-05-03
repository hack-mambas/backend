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
    [Route("api/v1/especialidades")]
    public class EspecialidadeController : ControllerBase
    {
        private readonly EspecialidadesService _service;
        
        public EspecialidadeController(EspecialidadesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Especialidade>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterTodas([FromQuery] string search)
        {
            var cidades = await _service.ObterTodas(search);

            return Ok(cidades);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Especialidade), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _service.ObterPorId(id);

            return Ok(retorno);
        }
    }
}