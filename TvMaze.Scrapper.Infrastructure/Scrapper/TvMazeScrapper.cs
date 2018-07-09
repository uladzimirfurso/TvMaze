using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TvMaze.ApiClient.Models;
using TvMaze.DataAccess.Domain.Entities;
using TvMaze.DataAccess.Domain.Repository;
using TvMaze.Scrapper.Domain.Interfaces.MazeClient;
using TvMaze.Scrapper.Domain.Interfaces.Scrapper;

namespace TvMaze.Scrapper.Infrastructure.Scrapper
{
    public class TvMazeScrapper : ITvMazeScrapper
    {
        private const int PageSize = 250;

        private readonly ITvMazeClient _tvMazeClient;
        private readonly ITvShowRepository _tvShowRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TvMazeScrapper(ITvMazeClient tvMazeClient, ITvShowRepository tvShowRepository, IMapper mapper, ILogger logger)
        {
            _tvMazeClient = tvMazeClient;
            _tvShowRepository = tvShowRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task ScrapAsync(CancellationToken token)
        {
            _logger.LogInformation("Scrapper started");

            var lastShow = await _tvShowRepository.GetLatestTvShowAsync();

            var pageNumber = (lastShow != null) ? GetPageNumber(lastShow.TvMazeId) : 0;
            var latestShowId = (lastShow != null) ? lastShow.TvMazeId : -1;

            while (!token.IsCancellationRequested)
            {
                _logger.LogInformation($"Scrapping page {pageNumber}");
                var shows = await _tvMazeClient.GetShowsAsync(pageNumber);

                if (shows == null ||!shows.Any()) break;

                var newTvShows = shows.Where(e => e.Id > latestShowId).OrderBy(e => e.Id);
                var newShows = new List<TvShow>();

                foreach (var tvShow in newTvShows)
                {
                    _logger.LogInformation($"Get cast for \"{tvShow.Name}\"(id:{tvShow.Id})");

                    var cast = await _tvMazeClient.GetShowCastAsync(tvShow);                    
                    if (cast == null)
                    {
                        _logger.LogInformation($"Fetch failed for cast for \"{tvShow.Name}\"(id:{tvShow.Id}).");
                        break;
                    }

                    tvShow.Cast = cast;

                    var newShow = _mapper.Map<MazeTvShow, TvShow>(tvShow);
                    newShows.Add(newShow);
                }

                _logger.LogInformation("Saving changes");
                await _tvShowRepository.AddTvShowsAsync(newShows);
                _logger.LogInformation("Changes saved");

                var latestShow = newShows.OrderBy(e => e.TvMazeId).Last();
                pageNumber = GetPageNumber(latestShow.TvMazeId);
                latestShowId = latestShow.TvMazeId;
            }

            _logger.LogInformation("Scrapping finished");
        }

        private static int GetPageNumber(int id)
        {
            return (int)Math.Ceiling((double)id / PageSize);
        }
    }
}
