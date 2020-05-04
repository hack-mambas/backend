using Healfi.Api.Application.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Healfi.Api.Server.Middlewares
{
    public static class GlobalErrorHandlingMiddleware
    {
        public static void UseGlobalErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(
                            JsonConvert.SerializeObject(new FailViewModel("Erro interno", contextFeature.Error.Message))
                        );
                    }
                });
            });
        }
    }
}