using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace GetCurrentWeather
{
    static class Program
    {


        static int Main(string[] args)
        {
            Option<string> cityOption = new Option<string>(
                new string[] {"--city", "-c"},
                "City name.")
            {
                IsRequired = true
            };

            Option<string> unitsOption = new Option<string>(
                aliases: new string[] {"--units", "-u"},
                description: "Units: metric or imperial.",
                getDefaultValue: () => "metric");


            RootCommand rootCommand = new RootCommand("An app for checking the weather.")
            {
                cityOption,
                unitsOption
            };

            rootCommand.Handler = CommandHandler.Create<string, string>((city, units) =>
            {
                Console.WriteLine($"{city}\n{units}");
            });

            return rootCommand.Invoke(args);
        }


        // Environmental variable with Open Weater API key.
        //private static readonly string _envVar = "OPENWEATHER_API_KEY";

        //private static readonly HttpClient _client = new HttpClient();


        /*static async Task Main(string[] args)
        {

            RootCommand cmd = new RootCommand
            {
                //new Argument<string>("name", "Your name."),
                new Option<string>(new[] { "--city", "-c" }, "The greeting to use."),
                new Option<string>(new[] { "--units", "-u" }, "The greeting to use."),
                //new Option("--help", "Display this help and exit,")
            };

            cmd.Handler = CommandHandler.Create<string, string, bool, IConsole>(HandleGreeting);

            int a = cmd.Invoke(args);
            Console.WriteLine(a);

            await ProcessRepositories(
                args.Length > 0 ? args[0] : "",
                System.Environment.GetEnvironmentVariable(_envVar));

        }


        private static async Task ProcessRepositories(string cityName, string apiKey)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}";
            Console.WriteLine(url);

            _client.DefaultRequestHeaders.Accept.Clear();

            //_client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            //_client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = _client.GetStringAsync(url);

            var msg = await stringTask;
            Console.WriteLine(msg);
        }*/
    }
}
