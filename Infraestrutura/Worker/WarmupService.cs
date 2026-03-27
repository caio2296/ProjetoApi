using Aplicacao.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infraestrutura.Worker
{
    public class WarmupService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public WarmupService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _provider.CreateScope();

                //var repoCal = scope.ServiceProvider
                //    .GetRequiredService<ICalendarAplicacao>();

                //await repoCal.BuscarCalendar();

                var repoFiltro = scope.ServiceProvider.GetRequiredService<IFiltroAplicacao>();

                await repoFiltro.BuscarFiltros(1);

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
