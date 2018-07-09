using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.ApiClient.Http;
using TvMaze.ApiClient.Models;
using TvMaze.ApiClient.Providers;
using TvMaze.Scrapper.Domain.Interfaces.MazeClient;

namespace TvMaze.TvMaze.Scrapper.Infrastructure.Client
{
    public class TvMazeClient : ITvMazeClient
    {   
        private readonly IRestClient _restClient;
        private readonly ITvMazeClientUrlProvider _tvMazeUrlProvider;
        
        public TvMazeClient(IRestClient restClient, ITvMazeClientUrlProvider tvMazeUrlProvider)
        {
            _restClient = restClient;
            _tvMazeUrlProvider = tvMazeUrlProvider;
        }

        public async Task<List<MazeTvActor>> GetShowCastAsync(MazeTvShow tvShow)
        {
            string url = _tvMazeUrlProvider.GetCastUrl(tvShow.Id);

            return await _restClient.GetContentAsync<List<MazeTvActor>>(url);            
        }

        public async Task<List<MazeTvShow>> GetShowsAsync(int page)
        {
            string url = _tvMazeUrlProvider.GetShowUrl(page);

            return await _restClient.GetContentAsync<List<MazeTvShow>>(url);
        }
    }
}
