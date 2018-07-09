using System.Collections.Generic;

namespace TvMaze.DataAccess.Domain.Entities
{
    public sealed class TvShow
    {
        public int TvShowId { get; set; }
        public int TvMazeId { get; set; }
        public string Name { get; set; }

        public ICollection<ActorTvShow> ActorTvShows { get; set; } = new List<ActorTvShow>();
    }
}
