using SingleAgent.Models;
using System.Globalization;

namespace SingleAgent.Plugins
{
    public class WeatherPlugin
    {
        public async Task<Weather> GetWeather(double latitude, double longitude)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");

            string endpoint = string.Format(CultureInfo.InvariantCulture, "forecast?latitude={0}&longitude={1}&current=temperature_2m", latitude, longitude);

            var result = await client.GetFromJsonAsync<Weather>(endpoint);
            return result;
        }
    }
}
