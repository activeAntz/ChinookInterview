using Chinook.Core.Helper;
using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Chinook.Utilities.Validation;
using Chinook.Services.Auth;
using NuGet.DependencyResolver;

namespace Chinook.Services
{
    public class PlayListService : IPlayListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string currentUserId;

        public PlayListService(IUnitOfWork unitOfWork, IAuthService auth)
        {
            _unitOfWork = unitOfWork;
            currentUserId = auth.CurrentUserId;
            
        }

        public (bool,long) AddNewPlaylist(string newPlayListName)
        {
            Guard.ThrowIfNull(newPlayListName);

            var playListCount = _unitOfWork.Playlists.Count();
            var newPlayList = _unitOfWork.Playlists.IncludeTracks(c => c.Name == newPlayListName && c.UserPlaylists.Any(x => x.UserId == currentUserId));

            if (newPlayList != null)
                return (false, newPlayList.PlaylistId);

            newPlayList = new Playlist
            {
                Name = newPlayListName,
                PlaylistId = playListCount + 1
            };

            var dataList = new UserPlaylist { UserId = currentUserId, Playlist = newPlayList };

            _unitOfWork.UserPlaylists.Add(dataList);
            if(_unitOfWork.Save() > 0)
                return (true, newPlayList.PlaylistId);

            return (false, newPlayList.PlaylistId);
        }

        public async Task<List<ClientModels.Playlists>> GetFilterPlaylistsAsync(long trackId)
        {
            Guard.ThrowIfNull(trackId);

            List<Playlist> playlists = await _unitOfWork.Playlists.IncludeTracksConditionAsync(p => p.UserPlaylists.Any(c => c.UserId == currentUserId) && !p.Tracks.Any(c => c.TrackId == trackId));

            return playlists.Select(c => new ClientModels.Playlists
            {
                playListId = c.PlaylistId,
                Name = c.Name
            }).ToList();
        }

        public async Task<ClientModels.Playlist> GetPlaylistAsync(long PlaylistId)
        {
            Guard.ThrowIfNull(PlaylistId);

            var playlists = await _unitOfWork.Playlists.ThenIncludeTracks(p => p.PlaylistId == PlaylistId && p.UserPlaylists.Any(c => c.UserId == currentUserId));
            var data = new ClientModels.Playlist
            {
                Name = playlists?.Name ?? string.Empty,
                Tracks = playlists.Tracks.Select(t => new ClientModels.PlaylistTrack()
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

        public IList<ClientModels.Playlists> GetPlaylistsByUserId()
        {
            var playlists = _unitOfWork.Playlists
                    .GetPlaylistsByUserId(p => p.UserPlaylists.Any(c => c.UserId == currentUserId));

            return playlists.Select(c => new ClientModels.Playlists
            {
                playListId = c.PlaylistId,
                Name = c.Name
            }).ToList();
        }
    }
}
