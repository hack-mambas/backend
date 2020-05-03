using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Healfi.Api.Application.Services.Abstracts;
using Healfi.Api.Data;
using Healfi.Api.Data.Extensions;
using Healfi.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Application.Services
{
    public class EspecialidadesService : BaseCrudService<Especialidade>
    {
        public EspecialidadesService(HealfiContext context) : base(context)
        {
        }

        public Task<List<Especialidade>> ObterTodas(string search)
        {
            var query = _context.Especialidades.Query();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.ToUpper().Contains(search.ToUpper().Trim()));
            }
            
            return query.ToListAsync();
        }
        

        public Task<Especialidade> ObterPorId(Guid id)
        {
            return _context.Especialidades.FindAsync(id).AsTask();
        }
    }
}