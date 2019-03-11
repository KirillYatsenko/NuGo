using System;

namespace EventBus.Abstracts
{
    public interface IIntegrationEvent
    {
        DateTime Created { get; set; }
    }
}