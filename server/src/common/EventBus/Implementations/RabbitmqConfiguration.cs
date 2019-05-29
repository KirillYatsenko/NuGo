using EventBus.Abstracts;

namespace EventBus.Implementations
{
    public class RabbitmqConfiguration : IRabbitmqConfiguration
    {
        public string Hostname { get; set; }
    }
}