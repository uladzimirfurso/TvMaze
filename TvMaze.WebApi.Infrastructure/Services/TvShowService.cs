using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TvMaze.ApiCore.Services;
using TvMaze.DataAccess.Domain.Entities;
using TvMaze.DataAccess.Domain.Repository;
using TvMaze.WebApi.Core.Entites;

namespace TvMaze.WebApi.Infrastructure.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly ITvShowRepository _tvShowRepository;
        private readonly IMapper _mapper;

        public TvShowService(ITvShowRepository repository, IMapper mapper)
        {
            _tvShowRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<ShowDto>> GetTvShowsAsync(int pageNumber, int pageSize)
        {
           var shows = await _tvShowRepository.ListTvShows(pageNumber, pageSize);
           var result = _mapper.Map<List<TvShow>, List<ShowDto>>(shows);
          
           return result;
        }
    }
}
