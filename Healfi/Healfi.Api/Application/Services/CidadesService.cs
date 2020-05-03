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
    public class CidadesService : BaseCrudService<Cidade>
    {
        public CidadesService(HealfiContext context) : base(context)
        {
        }

        public Task<List<Cidade>> ObterTodas(string search)
        {
            var query = _context.Cidades.Query();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.ToUpper().Contains(search.ToUpper().Trim()));
            }
            
            return query.ToListAsync();
        }
        
        
        public Task<List<Cidade>> ObterCidadesPorEstadoSigla(string estadoSigla)
        {
            return _context.Cidades.Query().Where(x => x.EstadoSigla.Equals(estadoSigla)).ToListAsync();
        }

        public Task<List<EstadoViewModel>> ObterEstados()
        {
            return _context.Cidades.Query()
                .Select(c => new EstadoViewModel()
                {
                    Nome = c.EstadoNome,
                    Sigla = c.EstadoSigla
                })
                .Distinct()
                .ToListAsync();
        }
    }
}