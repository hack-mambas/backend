using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services.Abstracts;
using Healfi.Api.Data;
using Healfi.Api.Data.Extensions;
using Healfi.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Application.Services
{
    public class ConsumidoresService : BaseCrudService<Consumidor>
    {
        public ConsumidoresService(HealfiContext context) : base(context)
        {
        }
        
        public async Task<ConsumidorViewModel> ObterPorId(Guid id)
        {
            var consumidor = await _context.Consumidores
                .Include(c => c.Enderecos)
                .ThenInclude(c => c.Endereco)
                .ThenInclude(c => c.Cidade)
                .Include(c => c.Usuario)
                .ThenInclude(c => c.Conquistas)
                .ThenInclude(c => c.Conquista)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consumidor == null) return null;

            return new ConsumidorViewModel()
            {
                Id = consumidor.Id,
                Conquistas = consumidor.Usuario.Conquistas?.Select(c => c.Conquista).ToList(),
                Enderecos = consumidor.Enderecos.Select(c => c.Endereco).ToList(),
                Email = consumidor.Usuario.Email,
                Genero = consumidor.Usuario.Genero,
                Nome = consumidor.Usuario.Nome,
                Telefone = consumidor.Usuario.PhoneNumber,
                DataNascimento = consumidor.Usuario.DataNascimento,
                GoogleId = consumidor.Usuario.GoogleId,
                LinkFotoPerfil = consumidor.Usuario.LinkFotoPerfil,
                TutorialRalizado = consumidor.Usuario.TutorialRalizado
            };
        }
    }
}