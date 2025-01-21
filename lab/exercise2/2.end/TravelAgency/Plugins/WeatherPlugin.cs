using Microsoft.SemanticKernel;
using TravelAgency.Models;
using System.ComponentModel;
using System.Globalization;

namespace TravelAgency.Plugins
{
    public class WeatherPlugin
    {
        [KernelFunction, Description("Get the current weather for a given location")]
        public async Task<Weather> GetTemperatureAsync([Description("The latitude of the location")]double latitude, [Description("The longitude of the location")]double longitude)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");

            string endpoint = string.Format(CultureInfo.InvariantCulture, "forecast?latitude={0}&longitude={1}&current=temperature_2m", latitude, longitude);

            var result = await client.GetFromJsonAsync<Weather>(endpoint);
            return result;
        }
    }
}
