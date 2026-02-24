using Aplicacao.Interface;
using Dominio.Servicos.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                var repoUsuario = scope.ServiceProvider.GetRequiredService<IUsuarioAplicacao>();

                await repoUsuario.ExisteUsuario("warmup@email.com");

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
