namespace EventWebScrapper.Models
{
    public class EventScrapData
    {
        public long Id { get; set; }
        public string DetailsUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }            
    }
}