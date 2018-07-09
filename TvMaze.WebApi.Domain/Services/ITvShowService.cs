using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvMaze.WebApi.Core.Entites;

namespace TvMaze.ApiCore.Services
{
    public interface ITvShowService
    {
        Task<List<ShowDto>> GetTvShowsAsync(int pageNumber, int pageSize);
    }
}
