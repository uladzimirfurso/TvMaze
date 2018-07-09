using System.Threading;
using System.Threading.Tasks;

namespace TvMaze.Scrapper.Domain.Interfaces.Scrapper
{
    public interface ITvMazeScrapper
    {
        Task ScrapAsync(CancellationToken token);
    }
}
