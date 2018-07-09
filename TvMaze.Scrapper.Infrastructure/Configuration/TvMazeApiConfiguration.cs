using TvMaze.Scrapper.Domain.Interfaces.MazeClient;

namespace TvMaze.Scrapper.Infrastructure.Configuration
{
    public class TvMazeApiConfiguration : ITvMazeClientConfiguration
    {        
        public double RateLimitSleepTimerSecs => 5;
        public int RateLimitRetryMaxCount => 10;
      
    }
}
