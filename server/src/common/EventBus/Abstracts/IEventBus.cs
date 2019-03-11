
namespace EventBus.Abstracts
{
    public interface IEventBus
    {
        void Publish(string exchangeName, string routingKey, object message);
        void Subscribe<T, K>(string exchangeName, string routingKey)
          where K : IIntegrationEvent
          where T : IEventHandler<K>;
    }
}