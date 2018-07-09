using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TvMaze.DataAccess.Domain.Entities;

namespace TvMaze.DataAccess
{
    public class TvMazeDbContext : DbContext
    {
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorTvShow> ActorTvShows { get; set; }
        public TvMazeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorTvShow>()
                .HasKey(t => new { t.ActorId, t.TvShowId });
        }
    }
}
