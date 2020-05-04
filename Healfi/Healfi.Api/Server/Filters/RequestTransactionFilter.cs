using System;
using System.Linq;
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
            var accepteds = new[]
            {
                typeof(OkResult),
                typeof(OkObjectResult),
                typeof(CreatedResult),
                typeof(NoContentResult)
            };
            
            if (accepteds.Any(x => context.Result.GetType() == x))
            {
                _context.SaveChanges();
            }
        }
    }
}