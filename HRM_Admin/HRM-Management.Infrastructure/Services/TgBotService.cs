using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using static HRM_Management.Core.Helpers.Constants;
using static HRM_Management.Core.Helpers.HelperMethods;
namespace HRM_Management.Infrastructure.Services
{
    public class TgBotService : IMessageService
    {
        private readonly HttpClient _blogHttpClient;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly string _routingKey;
        private IChannel _channel;

        public TgBotService(IHttpClientFactory httpClientFactory, IConnection connection, 
            IOptions<RabbitMQOptions> rabbitMqOptions, IOptions<HRM_TgBotApiOptions> hrmTgBotApiOptions)
        {
            _blogHttpClient = httpClientFactory.CreateClient(TgBotHttpClientName);
            _exchangeName = rabbitMqOptions.Value.ExchangeName;
            _routingKey = hrmTgBotApiOptions.Value.RoutingKey;
            _queueName = hrmTgBotApiOptions.Value.QueueName;
            Task.Run(() => EstablishConnectionAsync(connection)).Wait();
        }

        public async Task SendMessageAsync(string[] receivers, params SendingMessageDto[] messages)
        {
            var messageBuilder = new StringBuilder();
            foreach (var message in messages)
            {
                messageBuilder.AppendLine(message.MessageCotent);
            }

            var sendMessageRequest = new SendTgBotMessageDto
            {
                chatIds = receivers,
                Message = messageBuilder.ToString()
            };
            if (_channel.IsOpen)
                await SendMessageThroughRabbitAsync(sendMessageRequest);
            else
                await SendMessageThroughHttpAsync(sendMessageRequest);
        }
        
        private async Task SendMessageThroughHttpAsync(SendTgBotMessageDto request)
        {
            var content = CreateJsonContent(request, Encoding.UTF8);
            var result = await _blogHttpClient.PostAsync("Message/sendMessageToUser?message", content);
            result.EnsureSuccessStatusCode();
        }

        private async Task SendMessageThroughRabbitAsync(SendTgBotMessageDto request)
        {
            var jsonContent = JsonConvert.SerializeObject(request);
            var contentBytes = Encoding.UTF8.GetBytes(jsonContent);
            await _channel.BasicPublishAsync(_exchangeName, _routingKey, contentBytes);
        }

        private async Task EstablishConnectionAsync(IConnection connection)
        {
            _channel = await connection.CreateChannelAsync();
            await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct);
            await _channel.QueueDeclareAsync(_queueName, durable:true, exclusive:false, autoDelete: false);
            await _channel.QueueBindAsync(_queueName, _exchangeName, _routingKey);
        }

        ~TgBotService()
        {
            _channel.CloseAsync();
        }
    }
}
