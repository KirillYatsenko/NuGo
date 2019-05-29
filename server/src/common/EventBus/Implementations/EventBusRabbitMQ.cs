using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EventBus.Abstracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus.Implementations
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IRabbitmqConfiguration _configuration;
        private readonly ConnectionFactory _factory;
        private readonly ILifetimeScope _autofacScope;
        private const string SCOPE_NAME = "event_bus";

        public EventBusRabbitMQ(ILifetimeScope scope, IRabbitmqConfiguration configuration)
        {
            _autofacScope = scope;
            _configuration = configuration;

            _factory = new ConnectionFactory() { HostName = configuration.Hostname };
        }

        public void Publish(string exchangeName, string routingKey, object message)
        {
            using (var conneciton = _factory.CreateConnection())
            using (var channel = conneciton.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
                var serializedMessage = JsonConvert.SerializeObject(message);

                var messageFrame = Encoding.UTF8.GetBytes(serializedMessage);
                channel.BasicPublish(exchange: exchangeName,
                           routingKey: routingKey,
                           basicProperties: null,
                           body: messageFrame);

            }
        }

        public void Subscribe<T, K>(string exchangeName, string routingKey)
            where K : IIntegrationEvent
            where T : IEventHandler<K>
        {
            var conneciton = _factory.CreateConnection();
            var channel = conneciton.CreateModel();

            channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName,
                              exchange: exchangeName,
                              routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);

            subscribeHandler<T, K>(consumer);

            channel.BasicConsume(queue: queueName,
                           autoAck: true,
                           consumer: consumer); 
        }

        private void subscribeHandler<T, K>(EventingBasicConsumer consumer)
            where K : IIntegrationEvent
            where T : IEventHandler<K>
        {
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var messageString = message.ToString();

                var deserializedMessageObject = JsonConvert.DeserializeObject(messageString, typeof(K));
                var parsedObject = (K)deserializedMessageObject;

                using (var scope = _autofacScope.BeginLifetimeScope(SCOPE_NAME))
                {
                    var handlerObject = scope.ResolveOptional(typeof(T));
                    var parsedHandler = (IEventHandler<K>)handlerObject;
                    parsedHandler.Handle(parsedObject);
                }
            };
        }

    }
}