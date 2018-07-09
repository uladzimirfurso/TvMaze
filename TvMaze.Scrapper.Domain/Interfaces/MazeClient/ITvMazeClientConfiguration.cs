namespace TvMaze.Scrapper.Domain.Interfaces.MazeClient
{
    public interface ITvMazeClientConfiguration
    {             
        double RateLimitSleepTimerSecs { get; }
        int RateLimitRetryMaxCount { get; }
    }
}