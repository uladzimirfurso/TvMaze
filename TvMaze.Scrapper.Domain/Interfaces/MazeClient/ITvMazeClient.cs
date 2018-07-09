using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvMaze.ApiClient.Models;

namespace TvMaze.Scrapper.Domain.Interfaces.MazeClient
{
    public interface ITvMazeClient
    {
        Task<List<MazeTvShow>> GetShowsAsync(int page);
        Task<List<MazeTvActor>> GetShowCastAsync(MazeTvShow tvShow);
    }
}
