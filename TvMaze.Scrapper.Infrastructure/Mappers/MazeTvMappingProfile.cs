using AutoMapper;
using System.Linq;
using MoreLinq;
using TvMaze.ApiClient.Models;
using TvMaze.DataAccess.Domain.Entities;

namespace TvMaze.ApiClient.Mappers
{
    public class MazeTvMappingProfile : Profile
    {
        public MazeTvMappingProfile()
        {
            CreateMap<MazeTvActor, ActorTvShow>()
               .ForMember(d => d.Actor, o => o.MapFrom(x => x.Person));

            CreateMap<MazeTvShow, TvShow>()
                .ForMember(d => d.TvMazeId, o => o.MapFrom(x => x.Id))
                .ForMember(d => 
                    d.ActorTvShows, o => o.MapFrom(x => x.Cast.DistinctBy(p => p.Person.Id)));            

            CreateMap<MazeTvPerson, Actor>()
                .ForMember(d => d.TvMazeId, o => o.MapFrom(x => x.Id));

        }
    }
}
