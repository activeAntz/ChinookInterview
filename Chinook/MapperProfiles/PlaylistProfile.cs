using AutoMapper;
using Chinook.ClientModels;
using Chinook.Core.Data.Models;
using Chinook.Core.Helper;
using System.Reflection.Metadata;

namespace Chinook.MapperProfiles
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile() 
        {
            CreateMap<Playlist, PlaylistsDto>();
            //CreateMap<Playlist, PlaylistDto>()
            //     .ForMember(dest => dest.Tracks, opt => opt.MapFrom(c => c.Tracks.Select(c => new PlaylistTrackDto
            //     {
            //         AlbumTitle = c.Album?.Title ?? string.Empty,
            //         ArtistName = c.Album?.Artist.Name ?? string.Empty,
            //         TrackId = c.TrackId,
            //         TrackName = c.Name,
            //         IsFavorite = c.Playlists.Where(p => p.UserPlaylists != null && p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == FilterType.Favorites)).Any()
            //     })));
        }
    }
}
