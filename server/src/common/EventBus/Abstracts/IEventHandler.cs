using System.Threading.Tasks;

namespace EventBus.Abstracts
{
    public interface IEventHandler<in T>
    {
        Task Handle(T message);
    }
}