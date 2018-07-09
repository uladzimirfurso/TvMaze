namespace TvMaze.DataAccess.Domain.Entities
{
    public sealed class ActorTvShow
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; }
    }
}
