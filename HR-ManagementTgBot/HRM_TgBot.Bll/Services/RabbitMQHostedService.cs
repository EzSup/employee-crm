using HRM_TgBot.Bll.Options;
using HRM_TgBot.Core.DTOs;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Telegram.Bot.Requests;
namespace HRM_TgBot.Bll.Services
{
    public class RabbitMQHostedService : IHostedService, IDisposable, IAsyncDisposable
    {
        private IConnection _connection;
        private IChannel _channel;
        private readonly RabbitMQOptions _options;
        private readonly ILogger<RabbitMQHostedService> _logger;
        private string _consumerTag;
        private readonly IMessageService _messageService;

        public RabbitMQHostedService(IOptions<RabbitMQOptions> options,  ILogger<RabbitMQHostedService> logger, IMessageService messageService)
        {
            _options = options.Value;
            _logger = logger;
            _messageService = messageService;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                Port = _options.Port,
                UserName = _options.Username,
                Password = _options.Password
            };
            factory.ClientProvidedName = "TgBot-Consumer";
            _connection = await factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
            
            await _channel.ExchangeDeclareAsync(_options.ExchangeName, ExchangeType.Direct, cancellationToken: cancellationToken);
            await _channel.QueueDeclareAsync(_options.QueueName, durable:true, exclusive:false, autoDelete: false, cancellationToken: cancellationToken);
            await _channel.QueueBindAsync(_options.QueueName, _options.ExchangeName, _options.RoutingKey, cancellationToken: cancellationToken);
            await _channel.BasicQosAsync(0,1,false, cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                var body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                SendMessageDtoRequest request = JsonConvert.DeserializeObject<SendMessageDtoRequest>(message)!;
                foreach (var chat in request.ChatIds)
                {
                    await _messageService.SendTextMessageAsync(request.Message, chat);
                }
                await _channel.BasicAckAsync(args.DeliveryTag, false, cancellationToken);
            };
            
            _consumerTag = await _channel.BasicConsumeAsync(_options.QueueName, false, consumer, cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _channel.BasicCancelAsync(_consumerTag, cancellationToken: cancellationToken);
            await _channel.CloseAsync(cancellationToken: cancellationToken);
            await _connection.CloseAsync(cancellationToken: cancellationToken);
        }
        
        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
        
        public async ValueTask DisposeAsync()
        {
            await _connection.DisposeAsync();
            await _channel.DisposeAsync();
        }
    }
}
