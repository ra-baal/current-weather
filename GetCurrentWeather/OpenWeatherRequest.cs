using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GetCurrentWeather
{
    public static class OpenWeatherRequest
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<string> Send(string apiKey, string city, string units)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units={units}";

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Task<string> stringTask = _client.GetStringAsync(url);

            string response = await stringTask;

            return response;
        }

    }
}
