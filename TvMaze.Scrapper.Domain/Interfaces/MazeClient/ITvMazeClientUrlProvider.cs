namespace TvMaze.Scrapper.Domain.Interfaces.MazeClient
{
    public interface ITvMazeClientUrlProvider
    {
        string TvMazeUrl { get; }

        string GetCastUrl(int showId);

        string GetShowUrl(int pageId);
       
    }
}
