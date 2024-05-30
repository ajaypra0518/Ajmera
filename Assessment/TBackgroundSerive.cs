using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment
{
    public class TBackgroundSerive : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly IServiceProvider _services;
        private readonly IDbContextFactory<BookDbContext> _contextFactory;
        public TBackgroundSerive(IServiceProvider services, IServiceScopeFactory serviceFactory, IDbContextFactory<BookDbContext> contextFactor)
        {
            _services = services;
            _serviceFactory = serviceFactory;
            _contextFactory = contextFactor;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            System.Console.WriteLine("Inside background service method");
            using (var scope = _services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<BookDbContext>();

                var data = await scopedProcessingService.Books.ToListAsync();
                Console.WriteLine("Print IServiceProvider");
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }

            using (var scope = _serviceFactory.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<BookDbContext>();


                var data = await scopedProcessingService.Books.ToListAsync();
                Console.WriteLine("Print IServiceScopeFactory");
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }

            using (var context = _contextFactory.CreateDbContext()) 
            {
                var data = await context.Books.ToListAsync();
                Console.WriteLine("Print IDbContextFactory");
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }

        }
    }
}
