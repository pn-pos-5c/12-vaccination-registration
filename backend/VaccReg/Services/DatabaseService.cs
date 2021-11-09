using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VaccRegDb;

namespace VaccReg.Services
{
    public class DatabaseService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<DatabaseService> logger;

        public DatabaseService(IServiceScopeFactory scopeFactory, ILogger<DatabaseService> logger)
        {
            this.scopeFactory = scopeFactory;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(ParseJson, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ParseJson()
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<VaccRegContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (Program.Args.Length != 2 || Program.Args[0] != "import")
            {
                logger.Log(LogLevel.Information, "Invalid command line arguments!");
                return;
            }

            var json = File.ReadAllText($"Resources/{Program.Args[1]}");
            var registrations = JsonSerializer.Deserialize<List<Registration>>(json);
            db.Registrations.AddRange(registrations);
            db.SaveChanges();
        }
    }
}
