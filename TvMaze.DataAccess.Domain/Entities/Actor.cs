using System;
using System.Collections.Generic;

namespace TvMaze.DataAccess.Domain.Entities
{
    public sealed class Actor
    {
        public int ActorId { get; set; }
        public int TvMazeId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<ActorTvShow> ActorTvShows { get; set; } = new List<ActorTvShow>();
    }
}
