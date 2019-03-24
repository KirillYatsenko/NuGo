using System;
using CronScheduler.Enums;
using EventBus.Abstracts;

namespace CronScheduler.IntegrationEvents
{
    public class ScrapIntegrationEvent : IIntegrationEvent
    {
        public EventCategories Category { get; set; }
        public DateTime Created { get; set; }
    }
}