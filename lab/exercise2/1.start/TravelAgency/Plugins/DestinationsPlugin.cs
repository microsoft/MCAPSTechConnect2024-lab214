using Microsoft.SemanticKernel;
using SingleAgent.Models;

namespace SingleAgent.Plugins
{
    public class DestinationsPlugin
    {
        public List<Destination> GetDestinations(string[] tags)
        {
            List<Destination> destinations = new List<Destination>
            {
                new Destination
                {
                    Location = "Paris",
                    Country = "France",
                    Description = "Paris is the capital city of France. It is a major European city and a global center for art, fashion, gastronomy, and culture.",
                    Tags = new string[] { "City", "Culture", "Art", "Fashion" }
                },
                new Destination
                {
                    Location = "New York",
                    Country = "United States",
                    Description = "New York City is the largest city in the United States. It is known for its iconic landmarks, such as the Statue of Liberty, Times Square, and Central Park.",
                    Tags = new string[] { "City", "Culture", "Landmarks", "Shopping" }
                },
                new Destination
                {
                    Location = "Tokyo",
                    Country = "Japan",
                    Description = "Tokyo is the capital city of Japan. It is a vibrant metropolis known for its high-tech innovations, historic temples, and bustling street markets.",
                    Tags = new string[] { "City", "Technology", "Culture", "Food" }
                },
                new Destination
                {
                    Location = "Sydney",
                    Country = "Australia",
                    Description = "Sydney is the largest city in Australia. It is famous for its iconic Sydney Opera House, beautiful beaches, and vibrant nightlife.",
                    Tags = new string[] { "City", "Beaches", "Culture", "Nightlife" }
                },
                new Destination
                {
                    Location = "Rio de Janeiro",
                    Country = "Brazil",
                    Description = "Rio de Janeiro is a coastal city in Brazil known for its stunning beaches, vibrant carnival celebrations, and iconic Christ the Redeemer statue.",
                    Tags = new string[] { "City", "Beaches", "Culture", "Carnival" }
                },
                new Destination
                {
                    Location = "Cape Town",
                    Country = "South Africa",
                    Description = "Cape Town is a coastal city in South Africa known for its stunning natural beauty, diverse wildlife, and vibrant cultural scene.",
                    Tags = new string[] { "City", "Nature", "Wildlife", "Culture" }
                },
                new Destination
                {
                    Location = "Barcelona",
                    Country = "Spain",
                    Description = "Barcelona is a vibrant city in Spain known for its stunning architecture, beautiful beaches, and rich cultural heritage.",
                    Tags = new string[] { "City", "Architecture", "Beaches", "Culture" }
                },
                new Destination
                {
                    Location = "Dubai",
                    Country = "United Arab Emirates",
                    Description = "Dubai is a modern city in the United Arab Emirates known for its luxury shopping, futuristic skyscrapers, and vibrant nightlife.",
                    Tags = new string[] { "City", "Shopping", "Skyscrapers", "Nightlife" }
                }
            };

            var result = destinations
                .Where(d => tags.Any(tag => d.Tags.Contains(tag)))
                .ToList();

            return result;
        }
    }
}
