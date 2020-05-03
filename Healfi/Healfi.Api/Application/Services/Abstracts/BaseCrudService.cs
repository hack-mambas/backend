using Healfi.Api.Data;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Application.Services.Abstracts
{
    public abstract class BaseCrudService<TEntity> 
        where TEntity : BaseEntity
    {
        protected HealfiContext _context;

        protected BaseCrudService(HealfiContext context)
        {
            _context = context;
        }
        //
        // public (bool success, string message, TEntity entity) Add(TAddCommand command)
        // {
        //     
        // }
    }
}