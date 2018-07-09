using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TvMaze.ApiClient.Http;
using TvMaze.Scrapper.Domain.Interfaces.Json;
using TvMaze.Scrapper.Domain.Interfaces.MazeClient;

namespace TvMaze.Scrapper.Infrastructure.Http
{
    public class RestClient: IRestClient
    {
        private const int HttpStatusCodeReachedRateLimit = 429;

        private readonly ITvMazeClientConfiguration _tvMazeConfiguration;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly HttpClient _httpClient;

        public RestClient(ITvMazeClientConfiguration config, IJsonDeserializer jsonDeserializer, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _tvMazeConfiguration = config;
            _jsonDeserializer = jsonDeserializer;
        }
        public async Task<T> GetContentAsync<T>(string url)
        {
            var retryCount = 0;
            do
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return _jsonDeserializer.DeserializeObject<T>(content);
                }
                else if (response.StatusCode == (HttpStatusCode)HttpStatusCodeReachedRateLimit)
                {
                    retryCount++;
                    await Task.Delay(TimeSpan.FromSeconds(_tvMazeConfiguration.RateLimitSleepTimerSecs));
                }
                else
                {
                    return default(T);
                }
            }
            while (retryCount < _tvMazeConfiguration.RateLimitRetryMaxCount);

            return default(T);
        }
    }
}
