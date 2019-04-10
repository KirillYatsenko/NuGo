using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPageMVC.ViewModels
{
    public class Event
    {
        public Event() { }

        public long Id { get; set; }

        [Display(Name = "Event Url")]
        public string DetailsUrl { get; set; }

        public bool Deleted { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public EventCategory Category { get; set; }

        public List<EventSchedule> Schedules { get; set; }

        [Display(Name = "Closest Schedule")]
        public EventSchedule ClosestSchedule { get; set; }

        public IEnumerable<EventCategory> AllCategories { get; set; }

        [Display(Name = "Category")]
        public string CategoryName
        {
            get
            {
                return Category.Name;
            }
        }

        public decimal Rating { get; set; }
    }
}