using Dominio.DTO;
using Infraestrutura.SendEmail.Interface;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infraestrutura.SendEmail
{
    public class EmailProducer : IEmailProducer
{
    private readonly ConnectionFactory _factory;
        private readonly ILogger _logger;

        public EmailProducer() => _factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };



        public async Task EnviarAsync(EmailMessage mensagem)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "email-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var json = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "email-queue",
                body: body);
        }

        public async Task TentarEnviarEmailAsync(EmailMessage mensagem)
        {
            try
            {
                await EnviarAsync(mensagem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
