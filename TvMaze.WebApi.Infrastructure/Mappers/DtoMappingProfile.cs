using System.Linq;
using AutoMapper;
using TvMaze.DataAccess.Domain.Entities;
using TvMaze.WebApi.Core.Entites;

namespace TvMaze.WebApi.Infrastructure.Mappers
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {   
            CreateMap<Actor, ActorDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(x => x.TvMazeId));

            CreateMap<TvShow, ShowDto>()
                .ForMember(dto => dto.Cast, opt => opt.MapFrom(x => x.ActorTvShows.Select(s => s.Actor).OrderByDescending(a=>a.Birthday)))
                .ForMember(dto => dto.Id, opt => opt.MapFrom(x => x.TvMazeId));
                
        }
    }
}
