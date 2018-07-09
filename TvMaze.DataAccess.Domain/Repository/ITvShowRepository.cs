using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.DataAccess.Domain.Entities;

namespace TvMaze.DataAccess.Domain.Repository
{
    public interface ITvShowRepository
    {
        Task<TvShow> GetLatestTvShowAsync();
        Task AddTvShowsAsync(List<TvShow> tvShows);
        Task<List<TvShow>> ListTvShows(int pageNumber, int pageSize);
    }
}
