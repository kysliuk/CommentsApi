using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Comments.Host.Workers;

// Example BackgroundService shell – we'll flesh it out later.
public sealed class OutboxWorker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}
