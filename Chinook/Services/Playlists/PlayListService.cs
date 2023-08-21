using Chinook.Core.Helper;
using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace Chinook.Services
{
    public class PlayListService : IPlayListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public long AddNewPlaylist(string newPlayListName, string CurrentUserId)
        {
            var playListCount = _unitOfWork.Playlists.Count();
            var newPlayList = _unitOfWork.Playlists.IncludeTracks(c => c.Name == newPlayListName && c.UserPlaylists.Any(x => x.UserId == CurrentUserId));

            newPlayList = new Playlist
            {
                Name = newPlayListName,
                PlaylistId = playListCount + 1
            };

            var dataList = new UserPlaylist { UserId = CurrentUserId, Playlist = newPlayList };

            _unitOfWork.UserPlaylists.Add(dataList);
             _unitOfWork.Save();
            return newPlayList.PlaylistId;
        }

        public async Task<List<ClientModels.Playlists>> GetFilterPlaylistsAsync(long trackId, string CurrentUserId)
        {
            List<Playlist> playlists = await _unitOfWork.Playlists.IncludeTracksConditionAsync(p => p.UserPlaylists.Any(c => c.UserId == CurrentUserId) && !p.Tracks.Any(c => c.TrackId == trackId));

            return playlists.Select(c => new ClientModels.Playlists
            {
                playListId = c.PlaylistId,
                Name = c.Name
            }).ToList();
        }

        public async Task<ClientModels.Playlist> GetPlaylistAsync(long PlaylistId, string CurrentUserId)
        {
            var playlists = await _unitOfWork.Playlists.ThenIncludeTracks(p => p.PlaylistId == PlaylistId && p.UserPlaylists.Any(c => c.UserId == CurrentUserId));
            var data = new ClientModels.Playlist
            {
                Name = playlists.Name,
                Tracks = playlists.Tracks.Select(t => new ClientModels.PlaylistTrack()
                {
                    AlbumTitle = t.Album.Title,
                    ArtistName = t.Album.Artist.Name,
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists != null && p.UserPlaylists.Any(up => up.UserId == CurrentUserId && up.Playlist.Name == FilterType.Favorites)).Any()
                }).ToList()
            };

            return data;
        }

        public IList<ClientModels.Playlists> GetPlaylistsByUserId(string CurrentUserId)
        {
            return _unitOfWork.Playlists
                    .Where(p => p.UserPlaylists.Any(c => c.UserId == CurrentUserId))
                    .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
                    .Select(c => new ClientModels.Playlists
                    {
                        playListId = c.PlaylistId,
                        Name = c.Name
                    }).ToList();
        }
    }
}
