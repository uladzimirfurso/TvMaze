using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.ApiClient.Models
{
    public sealed class MazeTvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MazeTvActor> Cast { get; set; }
    }
}
