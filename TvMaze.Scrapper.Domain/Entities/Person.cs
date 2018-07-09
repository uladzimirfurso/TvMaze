using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.ApiClient.Models
{
    public sealed class MazeTvPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
