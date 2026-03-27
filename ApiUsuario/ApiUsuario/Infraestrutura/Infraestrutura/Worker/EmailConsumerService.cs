using Dominio.DTO;
using Dominio.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infraestrutura.Worker
{
    public class EmailConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        Console.WriteLine("🔌 Conectando ao RabbitMQ...");

                        var factory = new ConnectionFactory()
                        {
                            HostName = "localhost"
                        };

                        var connection = await factory.CreateConnectionAsync(stoppingToken);
                        var channel = await connection.CreateChannelAsync();

                        await channel.QueueDeclareAsync(
                            "email-queue",
                            true,
                            false,
                            false,
                            null);

                        Console.WriteLine("✅ Conectado ao RabbitMQ");

                        var consumer = new AsyncEventingBasicConsumer(channel);

                        consumer.ReceivedAsync += async (model, ea) =>
                        {
                            try
                            {
                                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                                var mensagem = JsonSerializer.Deserialize<EmailMessage>(json);

                                using var scope = _serviceProvider.CreateScope();
                                var emailService = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                                await emailService.SendAsync(mensagem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"❌ Erro ao processar mensagem: {ex.Message}");
                            }
                        };

                        await channel.BasicConsumeAsync(
                            "email-queue",
                            true,
                            consumer,
                            stoppingToken);

                        // mantém rodando sem travar startup
                        await Task.Delay(Timeout.Infinite, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"🔥 Erro RabbitMQ: {ex.Message}");

                        // tenta reconectar depois de 5s
                        await Task.Delay(5000, stoppingToken);
                    }
                }
            }, stoppingToken);

            return Task.CompletedTask;
        }
    }
}