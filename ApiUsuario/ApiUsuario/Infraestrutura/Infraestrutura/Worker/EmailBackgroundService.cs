using Aplicacao.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infraestrutura.Worker
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IEmailQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailBackgroundService(
            IEmailQueue queue,
            IServiceScopeFactory scopeFactory)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var usuario = await _queue.DequeueAsync(stoppingToken);

                using var scope = _scopeFactory.CreateScope();

                var emailService =
                    scope.ServiceProvider
                         .GetRequiredService<ISendEmailAplicacao>();

                const int maxTentativas = 3;

                for (int tentativa = 1; tentativa <= maxTentativas; tentativa++)
                {
                    try
                    {
                        await emailService.EnviarEmailAsync(usuario);
                        break; // sucesso → sai do loop
                    }
                    catch (Exception ex)
                    {
                        if (tentativa == maxTentativas)
                        {
                            // aqui você futuramente pode salvar em tabela de falhas
                            Console.WriteLine(
                                $"Falha definitiva ao enviar email para {usuario.Email}: {ex.Message}");
                        }
                        else
                        {
                            // backoff progressivo
                            int delayMs = tentativa * 3000; // 3s, 6s, 9s
                            await Task.Delay(delayMs, stoppingToken);
                        }
                    }
                }
            }
        }
    }
}
