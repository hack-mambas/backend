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
    public class CategoriaPadraosService : BaseCrudService<CategoriaPadrao>
    {
        public CategoriaPadraosService(HealfiContext context) : base(context)
        {
        }

        public Task<List<CategoriaPadrao>> ObterCategoriasPadrao(string search)
        {
            var query = _context.CategoriasPadrao.Query();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.ToUpper().Contains(search.ToUpper().Trim()));
            }
            
            return query.ToListAsync();
        }
        

        public Task<CategoriaPadrao> ObterPorId(Guid id)
        {
            return _context.CategoriasPadrao.FindAsync(id).AsTask();
        }
    }
}