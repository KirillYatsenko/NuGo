using System;

namespace AdminPageMVC.ViewModels
{
    public class EventSchedule
    {
        public int Id { get; set; }
        public Event Event { get; set; }

        public PriceInfo Price { get; set; }

        public DateTime Date { get; set; }

        public bool Deleted { get; set; }

        public string HostName { get; set; }

        public string Address { get; set; }
    }
}