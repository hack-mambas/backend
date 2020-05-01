using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Healfi.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Healfi.Api.Server.Middlewares
{
    public static class MigrationMiddlewareConfig
    {
        public static IApplicationBuilder UseMigrationVerifier(this IApplicationBuilder app)
        {
            app.UseMiddleware<MigrationMiddleware>();
            return app;
        }
    }
    
    public class MigrationMiddleware
    {
        private readonly RequestDelegate _next;

        public MigrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, HealfiContext context, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                await context.Database.EnsureDeletedAsync();
            }
            await context.Database.MigrateAsync();
        }
    }
}