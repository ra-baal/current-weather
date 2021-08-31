using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace GetCurrentWeather
{
    public static class Weather
    {
        private static readonly HttpClient _client = new HttpClient();
        private static string _response;

        public static async Task SendRequest(string apiKey, string city, string units)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units={units}&mode=xml";
            Console.WriteLine(url);

            _client.DefaultRequestHeaders.Accept.Clear();

            //_client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            //_client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = _client.GetStringAsync(url);

            _response = await stringTask;
            //Console.WriteLine(_response);
        }

        private static void Process()
        {
        }

        public static string ProcessedData()
        {
            Process();
            return "pogoda";
        }
    }
}
