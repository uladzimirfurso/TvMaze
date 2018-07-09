using TvMaze.Scrapper.Domain.Interfaces.MazeClient;

namespace TvMaze.ApiClient.Providers
{
    public class TvMazeClientUrlProvider: ITvMazeClientUrlProvider
    {
        public string TvMazeUrl => "http://api.tvmaze.com";

        public string GetCastUrl(int showId)
        {
            return $"{TvMazeUrl}/shows/{showId}/cast";
        }
        public string GetShowUrl(int pageId)
        {
            return $"{TvMazeUrl}/shows?page={pageId}";
        }
    }
}
