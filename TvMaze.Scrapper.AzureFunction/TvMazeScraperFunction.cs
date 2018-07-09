using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TvMaze.Scrapper.Domain.Interfaces.Scrapper;

namespace TvMaze.ScrapperAzureFunction
{
    public static class TvMazeScraperFunction
    {
        [FunctionName("TvMazeScraper")]
        public static async Task Run([TimerTrigger("0 0 * * * *")]TimerInfo timer, ILogger logger, CancellationToken token)
        {
            logger.LogInformation($"Scraping TvMaze : {DateTime.UtcNow}");

            var config = GetConfiguration();

            var container = IoCBuilder.BuildIocContainer(config, logger);

            var scraper = container.GetInstance<ITvMazeScrapper>();

            await scraper.ScrapAsync(token);

            logger.LogInformation($"Scraping TvMaze finished : {DateTime.UtcNow}");
        }

        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
