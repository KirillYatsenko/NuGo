using AdminPageMVC.ViewModels;

namespace AdminPageMVC.ViewModels
{
    public class PriceInfo
    {
        public int Id { get; set; }

        public int EventDateId { get; set; }

        public decimal Min { get; set; }

        public decimal Max { get; set; }

        public string Currency { get; set; }
    }
}