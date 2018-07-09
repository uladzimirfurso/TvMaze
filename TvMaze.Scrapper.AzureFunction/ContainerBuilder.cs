using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using System.Net.Http;
using TvMaze.ApiClient.Http;
using TvMaze.ApiClient.Mappers;
using TvMaze.ApiClient.Providers;
using TvMaze.DataAccess;
using TvMaze.DataAccess.Domain.Repository;
using TvMaze.DataAccess.Infrastructure.Repository;
using TvMaze.Scrapper.Domain.Interfaces.Json;
using TvMaze.Scrapper.Domain.Interfaces.MazeClient;
using TvMaze.Scrapper.Domain.Interfaces.Scrapper;
using TvMaze.Scrapper.Infrastructure.Configuration;
using TvMaze.Scrapper.Infrastructure.Http;
using TvMaze.Scrapper.Infrastructure.Json;
using TvMaze.Scrapper.Infrastructure.Scrapper;
using TvMaze.TvMaze.Scrapper.Infrastructure.Client;

namespace TvMaze.ScrapperAzureFunction
{
    static class IoCBuilder
    {
        public static Container BuildIocContainer(IConfigurationRoot configurationRoot, ILogger logger)
        {
            var container = new Container();
            container.Register<ITvMazeClient, TvMazeClient>();
            container.Register<ITvShowRepository, TvShowRepository>();
            container.Register<ITvMazeClientConfiguration, TvMazeApiConfiguration>();
            container.Register<IRestClient, RestClient>();
            container.Register<ITvMazeScrapper, TvMazeScrapper>();
            container.Register<IJsonDeserializer, JsonDeserializer>();

            container.Register<TvMazeDbContext>(() =>
            {
                var builder = new DbContextOptionsBuilder();                
                builder.UseSqlServer(configurationRoot.GetConnectionString("Default"));
                return new TvMazeDbContext(builder.Options);
            });

            container.Register<ITvMazeClientUrlProvider, TvMazeClientUrlProvider>();

            container.RegisterInstance(new HttpClient());

            container.RegisterInstance<IMapper>(new Mapper(CreateMapperConfiguration()));

            return container;
        }

        private static MapperConfiguration CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(MazeTvMappingProfile));
            });

            return config;
        }
    }
}
