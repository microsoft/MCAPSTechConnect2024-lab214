namespace TravelAgency.Models
{
    public class Destination
    {
        public string Location { get; set; }
        public string Country { get; set; }

        public string Description { get; set; }
        public  string[] Tags { get; set; }
    }
}
