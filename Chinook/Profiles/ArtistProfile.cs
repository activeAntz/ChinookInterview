using Chinook.Core.Data.Models;
using AutoMapper;
using Chinook.ClientModels;

namespace Chinook.Profiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<Artist, ArtistsDto>()
                 .ForMember(dest => dest.AlbumsCount, opt => opt.MapFrom(c => c.Albums.Count()));
        }
    }
}
