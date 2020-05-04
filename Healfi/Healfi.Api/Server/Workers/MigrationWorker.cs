using Healfi.Api.Data;
using Healfi.Api.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Healfi.Api.Server.Workers
{
    public class MigrationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostEnvironment _environment;

        public MigrationWorker(IServiceProvider sp, IHostEnvironment env)
        {
            _serviceProvider = sp;
            _environment = env;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(new TimeSpan(0, 0, 45), stoppingToken);
            
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            
            await using var healfiContext = services.GetRequiredService<HealfiContext>();
            
            try
            {
                if (_environment.IsDevelopment())
                {
                    await healfiContext.Database.EnsureDeletedAsync(stoppingToken);
                }
            
                await healfiContext.Database.MigrateAsync(stoppingToken);
            
                await healfiContext.SeedDefaultData(stoppingToken);
                
                Console.WriteLine("Migrate success");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{typeof(MigrationWorker).Name}: Error executing");
                Console.WriteLine(e.Message);
            }
        }
    }
}
