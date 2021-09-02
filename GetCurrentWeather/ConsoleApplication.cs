using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GetCurrentWeather
{
    static class ConsoleApplication
    {
        private enum Units
        {
            metric,
            imperial
        }

        // Environmental variable with Open Weater API key.
        private static readonly string _envVar = "OPENWEATHER_API_KEY";

        static async Task<int> Main(string[] args)
        {
            (string city, string units) = ParseArgs(args);
            string apiKey = GetApiKey();

            string jsonStr = await SendRequestAsync(apiKey, city, units);
            string result = Process(jsonStr);

            Console.WriteLine(result);

            return 0;
        }

        private static (string, string) ParseArgs(string[] args)
        {

            var cityOption = new Option<string>(
                new string[] { "--city", "-c" },
                "A city name.")
            {
                IsRequired = true
            };

            var unitsOption = new Option<Units>(
                aliases: new string[] { "--units", "-u" },
                description: "An unit.",
                getDefaultValue: () => Units.metric);

            RootCommand rootCommand = new RootCommand("An app for checking the weather.")
            {
                cityOption,
                unitsOption
            };

            (string city, string units) parsedArgs = ("", "");

            rootCommand.Handler = CommandHandler.Create<string, Units>((city, units) =>
            {
                parsedArgs.city = city;
                parsedArgs.units = units.ToString();
            });

            int exitCode = rootCommand.Invoke(args);

            if (exitCode != 0)
                System.Environment.Exit(exitCode);

            return parsedArgs;
        }

        private static string GetApiKey()
        {
            string apiKey = Environment.GetEnvironmentVariable(_envVar);

            if (apiKey == null)
            {
                Console.WriteLine("Set the OPENWEATHER_API_KEY variable.");
                System.Environment.Exit(2);
            }

            return apiKey;
        }

        private static async Task<string> SendRequestAsync(string apiKey, string city, string units)
        {
            string jsonStr = "";

            try
            {
                jsonStr = await OpenWeatherRequest.Send(apiKey, city, units);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Console.WriteLine("Not found.");
                System.Environment.Exit(3);
            }

            return jsonStr;

        }

        private static string Process(string jsonStr)
        {
            JObject jsonObj = JObject.Parse(jsonStr);
            string city = "", temperature = "", wind_speed = "", humidity = "", pressure = "";

            try
            {
                city = jsonObj.SelectToken("name").ToString();
                temperature = jsonObj.SelectToken("main.temp").ToString();
                wind_speed = jsonObj.SelectToken("wind.speed").ToString();
                humidity = jsonObj.SelectToken("main.humidity").ToString();
                pressure = jsonObj.SelectToken("main.pressure").ToString();
            }
            catch (NullReferenceException)
            {
                System.Environment.Exit(4);
            }
        
            return $"{city}|{temperature}|{wind_speed}|{humidity}|{pressure}";
        }

    }
}
