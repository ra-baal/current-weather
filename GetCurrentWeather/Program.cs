using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetCurrentWeather
{
    static class Program
    {
        static int _exitCode = 0;

        static async Task<int> Main(string[] args)
        {
            (string city, string units) = parseArgs(args);
            string apiKey = getApiKey();

            if (_exitCode == 0)
            {
                await Weather.SendRequest(apiKey, city, units);
                string output = Weather.ProcessedData();

                Console.WriteLine(output);
            }

            return _exitCode;
        }

        enum Units
        {
            metric,
            imperial
        }

        private static (string, string) parseArgs(string[] args)
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

            _exitCode = rootCommand.Invoke(args);

            return parsedArgs;
        }

        // Environmental variable with Open Weater API key.
        private static readonly string _envVar = "OPENWEATHER_API_KEY";

        private static string getApiKey()
        {
            string apiKey = Environment.GetEnvironmentVariable(_envVar);

            if (apiKey == null)
                _exitCode = 2;

            return apiKey;
        }


    }
}
