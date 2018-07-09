using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMaze.ApiCore.Services;
using TvMaze.WebApi.Core.Entites;

namespace TvMaze.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowController : ControllerBase
    {
        private readonly ITvShowService _tvShowService;

        public TvShowController(ITvShowService tvShowService)
        {
            _tvShowService = tvShowService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ShowDto>>> List(int pageNumber = 0, int pageSize = 250)
        {
            if (pageNumber < 0) return BadRequest("page number should be non negative");
            if (pageSize <= 0) return BadRequest("Page Size should be greater that zero");

            var shows = await _tvShowService.GetTvShowsAsync(pageNumber, pageSize);

            if (!shows.Any()) return NotFound(shows);

            return Ok(shows);
        }

    }
}
