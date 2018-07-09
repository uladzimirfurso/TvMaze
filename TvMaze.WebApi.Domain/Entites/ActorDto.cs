using System;
using Newtonsoft.Json;
using TvMaze.WebApi.Core.Formatters;

namespace TvMaze.WebApi.Core.Entites
{
    public class ActorDto
    {
        public int Id { get; set; }        
        public string Name { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? Birthday { get; set; }
    }
}
