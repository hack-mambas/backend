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
    public class FormasEntregaService : BaseCrudService<FormaEntrega>
    {
        public FormasEntregaService(HealfiContext context) : base(context)
        {
        }

        public Task<List<FormaEntrega>> ObterTodas(string search)
        {
            var query = _context.FormasEntrega.Query();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.ToUpper().Contains(search.ToUpper().Trim()));
            }
            
            return query.ToListAsync();
        }
        

        public Task<FormaEntrega> ObterPorId(Guid id)
        {
            return _context.FormasEntrega.FindAsync(id).AsTask();
        }
    }
}