using System.Collections.Generic;


namespace TvMaze.WebApi.Core.Entites
{
    public class ShowDto
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ActorDto> Cast { get; set; }
    }
}
