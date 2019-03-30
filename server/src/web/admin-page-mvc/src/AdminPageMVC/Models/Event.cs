using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPageMVC.Models
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

        public int CategoryId { get; set; }
        public EventCategory Category { get; set; }

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