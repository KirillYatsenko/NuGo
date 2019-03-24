using System;
using EventBus.Abstracts;
using EventWebScrapper.Enums;

namespace EventWebScrapper.IntegrationEvents
{
    public class ScrapIntegrationEvent : IIntegrationEvent
    {
        public EventCategories Category { get; set; }
        public DateTime Created { get; set; }
    }
}