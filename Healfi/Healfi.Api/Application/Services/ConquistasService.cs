using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Healfi.Api.Application.Services.Abstracts;
using Healfi.Api.Data;
using Healfi.Api.Data.Extensions;
using Healfi.Api.Domain;
using Healfi.Api.Domain.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Application.Services
{
    public class ConquistasService : BaseCrudService<Conquista>
    {
        public ConquistasService(HealfiContext context) : base(context)
        {
        }

        public Task<List<Conquista>> ObterTodas(TipoVinculoEnum? tipo, string search)
        {
            var query = _context.Conquistas.Query();

            if (tipo.HasValue)
            {
                query = query.Where(c => c.Tipo == tipo);
            }
            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.ToUpper().Contains(search.ToUpper().Trim()) || c.CondicaoConquista.ToUpper().Contains(search.ToUpper().Trim()));
            }
            
            return query.ToListAsync();
        }
        

        public Task<Conquista> ObterPorId(Guid id)
        {
            return _context.Conquistas.FindAsync(id).AsTask();
        }
    }
}