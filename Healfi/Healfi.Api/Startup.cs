using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services;
using Healfi.Api.Data;
using Healfi.Api.Domain;
using Healfi.Api.Server.Filters;
using Healfi.Api.Server.Middlewares;
using Healfi.Api.Server.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Healfi.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Banco de dados
            services.AddDbContext<HealfiContext>((opt) => opt.UseNpgsql(_configuration.GetConnectionString("HealfiDB")));

            // Segurança
            services
                .AddDefaultIdentity<Usuario>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;
                })
                .AddEntityFrameworkStores<HealfiContext>()
                .AddDefaultTokenProviders();

            services
                .AddHttpContextAccessor()
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Security:Secret"])),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false
                    };
                });

            // Documentação
            services.AddSwaggerGen(cfg =>
            {
                var contact = new OpenApiContact
                {
                    Name = "Healfi Restful Api",
                    Email = "healfi@gmail.com.br",
                    Url = new Uri("https://www.healfi.com.br")
                };

                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Healfi API 1.0",
                    Contact = contact
                });

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Copie 'Bearer ' + token'",
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

            });

            // Configurações gerais da api
            services
                .Configure<ApiBehaviorOptions>(o =>
                {
                    o.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new FailViewModel("Dados inválidos", actionContext.ModelState.SelectMany(x => x.Value.Errors).ElementAtOrDefault(0)?.ErrorMessage));
                })
                .AddControllers()
                .AddMvcOptions((opt) =>
                {
                    opt.Filters.Add(typeof(RequestTransactionFilter<HealfiContext>));
                })
                .AddJsonOptions((opt) =>
                {
                    opt.JsonSerializerOptions.IgnoreNullValues = false;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddHttpContextAccessor();
            services.AddHostedService<MigrationWorker>();
            services.AddScoped<AuthService>();
            services.AddScoped<CidadesService>();
            services.AddScoped<CategoriaPadraosService>();
            services.AddScoped<ConquistasService>();
            services.AddScoped<ConsumidoresService>();
            services.AddScoped<EspecialidadesService>();
            services.AddScoped<ProdutoresService>();
            services.AddScoped<CategoriasService>();
            services.AddScoped<ProdutosService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalErrorHandler();
            
            app.UseCors(c => c
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );

            app.UseAuthentication();
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app
                .UseSwagger()
                .UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                    s.DisplayRequestDuration();
                    s.DisplayOperationId();
                    s.EnableValidator();
                    s.DefaultModelRendering(ModelRendering.Model);
                    s.DocExpansion(DocExpansion.List);
                    s.EnableDeepLinking();
                    s.ShowExtensions();

                    s.RoutePrefix = "swagger";
                    s.DocumentTitle = "Healfi - Restful Api";
                });
        }
    }
}
