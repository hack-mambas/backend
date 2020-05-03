using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Healfi.Api.Domain;
using Healfi.Api.Domain.Enumerators;
using Microsoft.AspNetCore.Mvc;

namespace Healfi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/conquistas")]
    public class ConquistaController : ControllerBase
    {
        private readonly ConquistasService _service;
        
        public ConquistaController(ConquistasService service)
        {
            _service = service;
        }

        /// <summary>
        /// TIPO: 1 PARA CONSUMIDOR - 2 PARA PRODUTOR - 3 OU NULLO PARA AMBOS 
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Conquista>), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterTodas([FromQuery] TipoVinculoEnum? tipo, [FromQuery] string search)
        {
            var cidades = await _service.ObterTodas(tipo, search);

            return Ok(cidades);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Conquista), 200)]
        [ProducesResponseType(typeof(FailViewModel), 400)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _service.ObterPorId(id);

            return Ok(retorno);
        }
    }
}