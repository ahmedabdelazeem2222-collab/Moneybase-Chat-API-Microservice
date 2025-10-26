using Microsoft.Extensions.Logging;
using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Shared;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MoneyBase.Support.Infrastructure.MessageBus
{
    public class RabbitMqChatProducer : IChatProducer, IDisposable
    {
        #region Fields
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;
        private readonly string _queueName = "chat_queue";
        private readonly ILogger<RabbitMqChatProducer> _logger;
        public RabbitMqChatProducer(string host, string user, string pass, ILogger<RabbitMqChatProducer> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory { HostName = host, UserName = user, Password = pass };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        }
        #endregion

        #region Methods
        public Task PublishAsync(ChatRequestDto request, CancellationToken ct = default)
        {
            try
            {
                _logger.LogInformation($"RabbitMqChatProducer/PublishAsync - start pushing message to the queue: {_queueName}");
                request.CreatedDate = DateTime.UtcNow;
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
                var props = _channel.CreateBasicProperties();
                props.Persistent = true;
                _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: props, body: body);
                _logger.LogInformation($"RabbitMqChatProducer/PublishAsync - end pushing message to the queue: {_queueName}");
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "RabbitMqChatProducer/PublishAsync Error.");
                return Task.FromException(ex);
            }
        }
        public void Dispose()
        {
            try { _channel?.Close(); _connection?.Close(); } catch { }
        }
        #endregion

    }
}
