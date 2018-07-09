using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using TvMaze.DataAccess.Domain.Entities;
using TvMaze.DataAccess.Domain.Repository;

namespace TvMaze.DataAccess.Infrastructure.Repository
{ 
    public class TvShowRepository : ITvShowRepository
    {
        private readonly TvMazeDbContext _dbContext;

        public TvShowRepository(TvMazeDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddTvShowsAsync(List<TvShow> tvShows)
        {
            var actors = tvShows.SelectMany(e => e.ActorTvShows.Select(_ => _.Actor))
                                .DistinctBy(p => p.TvMazeId)
                                .ToList();
            
            await SaveActors(actors);
            await SaveTvShow(tvShows, actors);
        }

        public Task<TvShow> GetLatestTvShowAsync()
        {
            return _dbContext.TvShows
                            .AsNoTracking()
                            .OrderByDescending(e => e.TvMazeId)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<TvShow>> ListTvShows(int pageNumber, int pageSize)
        {
            return await _dbContext.TvShows
                        .AsNoTracking()
                        .Include(e => e.ActorTvShows)
                        .ThenInclude(a => a.Actor)
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        private async Task SaveActors(List<Actor> actors)
        {
            var actorIds = actors.Select(a => a.TvMazeId);
            var existingActors = await _dbContext.Actors.Where(e => actorIds.Contains(e.TvMazeId)).Select(a => a.TvMazeId).ToListAsync();

            var newActors = actors               
            .Where(e => !existingActors.Contains(e.TvMazeId))
            .Select(actor => new Actor()
            {
                Birthday = actor.Birthday,
                TvMazeId = actor.TvMazeId,
                Name = actor.Name
            });

            foreach (var actor in newActors)
            {
                _dbContext.Actors.Add(actor);
            }

            await _dbContext.SaveChangesAsync();            
        }

        private async Task SaveTvShow(List<TvShow> tvShows, List<Actor> actors)
        {
            var actorIds = actors.Select(a => a.TvMazeId);
            // get all actors to bind to db
            var allExistingActors = await _dbContext.Actors.Where(e => actorIds.Contains(e.TvMazeId)).ToDictionaryAsync(a => a.TvMazeId);

            //Save tvShows with references to actors
            foreach (var show in tvShows)
            {
                foreach (var actor in show.ActorTvShows)
                {
                    var mazeId = actor.Actor.TvMazeId;
                    actor.Actor = allExistingActors[mazeId];
                }

                _dbContext.TvShows.Add(show);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
