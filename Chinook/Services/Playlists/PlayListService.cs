using Chinook.Core.Helper;
using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Chinook.Utilities.Validation;
using Chinook.Services.Auth;
using Chinook.ClientModels;
using AutoMapper;

namespace Chinook.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string currentUserId;
        private readonly IMapper _mapper;

        public event Action ClientEventCallBack;

        public PlaylistService(IUnitOfWork unitOfWork, IAuthService auth, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            currentUserId = auth.CurrentUserId;
            _mapper = mapper;
        }

        public (bool, long) AddPlaylist(string playlistName)
        {
            Guard.ThrowIfNull(playlistName);

            var playListCount = _unitOfWork.Playlists.Count();
            var newPlayList = _unitOfWork.Playlists.IncludeTracks(c => c.Name == playlistName && c.UserPlaylists.Any(x => x.UserId == currentUserId));

            if (newPlayList != null)
                return (false, newPlayList.PlaylistId);

            newPlayList = new Playlist
            {
                Name = playlistName,
                PlaylistId = playListCount + 1
            };

            var dataList = new UserPlaylist { UserId = currentUserId, Playlist = newPlayList };

            _unitOfWork.UserPlaylists.Add(dataList);
            if (_unitOfWork.Save() > 0)
                return (true, newPlayList.PlaylistId);

            ClientEventCallBack.Invoke();

            return (false, newPlayList.PlaylistId);
        }

        public async Task<List<PlaylistsDto>> GetFilterPlaylistsByTrackIdAsync(long trackId)
        {
            Guard.ThrowIfNull(trackId);

            var playlists = await _unitOfWork.Playlists.IncludeTracksWithConditionAsync(p => p.UserPlaylists.Any(c => c.UserId == currentUserId) && !p.Tracks.Any(c => c.TrackId == trackId));

            var mapPlaylists = _mapper.Map<List<PlaylistsDto>>(playlists);

            return mapPlaylists;
        }

        public async Task<PlaylistDto> GetPlaylistByIdAsync(long id)
        {
            Guard.ThrowIfNull(id);

            var playlists = await _unitOfWork.Playlists.ThenIncludeTracks(p => p.PlaylistId == id && p.UserPlaylists.Any(c => c.UserId == currentUserId));

            var data = new PlaylistDto
            {
                Name = playlists.Name,
                Tracks = playlists.Tracks.Select(t => new PlaylistTrackDto()
                {
                    AlbumTitle = t.Album?.Title ?? string.Empty,
                    ArtistName = t.Album?.Artist.Name ?? string.Empty,
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists != null && p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == FilterType.Favorites)).Any()
                }).ToList()
            };

            return data;
        }

        public async Task<List<PlaylistsDto>> GetPlaylistsAsync()
        {
            var playlists = await _unitOfWork.Playlists
                    .GetPlaylistsByUserIdAsync(p => p.UserPlaylists.Any(c => c.UserId == currentUserId));

            var mapPlaylists = _mapper.Map<List<PlaylistsDto>>(playlists);

            return mapPlaylists;
        }
    }
}
