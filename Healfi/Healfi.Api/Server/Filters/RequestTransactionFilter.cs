using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Server.Filters
{
    public class RequestTransactionFilter<TContext> : IActionFilter where TContext : DbContext
    {
        private readonly TContext _context;

        public RequestTransactionFilter(TContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is OkResult || context.Result is CreatedResult)
            {
                _context.SaveChanges();
            }
        }
    }
}