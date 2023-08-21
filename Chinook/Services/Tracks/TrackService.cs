using Chinook.ClientModels;
using Chinook.Core.Helper;
using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Linq;
using System.Net.NetworkInformation;
using Chinook.Services.Auth;
using Chinook.Utilities.Validation;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string currentUserId;

        public TrackService(IUnitOfWork unitOfWork, IAuthService auth)
        {
            _unitOfWork = unitOfWork;
             currentUserId = auth.CurrentUserId;
        }
        public List<PlaylistTrack> GetPlaylistTracksByArtistId(long artistId)
        {
            Guard.ThrowIfNull(artistId);

            var Artist = _unitOfWork.Artists.Get(a => a.ArtistId == artistId);

            return _unitOfWork.Tracks.Where(a => a.Album.ArtistId == artistId)
                        .Include(a => a.Album)
                        .Select(t => new PlaylistTrack()
                        {
                            AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                            TrackId = t.TrackId,
                            TrackName = t.Name,
                            IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == FilterType.Favorites)).Any()
                        })
                        .ToList();
        }

        public int AddFavoriteTrack(long trackId)
        {
            Guard.ThrowIfNull(trackId);

            var playListCount = _unitOfWork.Playlists.Count();

            var playList = _unitOfWork.Playlists.Get(c => c.Name == FilterType.Favorites && c.UserPlaylists.Any(x => x.UserId == currentUserId));
            var selectedTrack = _unitOfWork.Tracks.Get(a => a.TrackId == trackId);

            if (playList != null && selectedTrack != null)
            {
                selectedTrack.Playlists.Add(playList);
                playList.Tracks.Add(selectedTrack);
            }
            else
            {
                playList = new Chinook.Core.Data.Models.Playlist
                {
                    Name = FilterType.Favorites,
                    PlaylistId = playListCount + 1
                };
                _unitOfWork.Playlists.Add(playList);

                selectedTrack.Playlists.Add(playList);
                playList.Tracks.Add(selectedTrack);

                var dataList = new UserPlaylist { UserId = currentUserId, Playlist = playList };
                _unitOfWork.UserPlaylists.Add(dataList);
            }

           return _unitOfWork.Save();
        }

        public (bool, string?) RemoveTrack(long trackId, long? playlistId = null)
        {
            Guard.ThrowIfNull(trackId);

            var playList = _unitOfWork.Playlists.IncludeTracks(c => (playlistId.HasValue ? c.PlaylistId == playlistId : c.Name == FilterType.Favorites) && c.UserPlaylists.Any(x => x.UserId == currentUserId));
            var selectedTrack = _unitOfWork.Tracks.IncludePlayLists(a => a.TrackId == trackId);

            if (playList != null && selectedTrack != null)
            {
                selectedTrack.Playlists.Remove(playList);
                playList.Tracks.Remove(selectedTrack);

                var isSave = _unitOfWork.Save();

                return (isSave == 1, playList.Name);
            }

            return (false, playList.Name);
        }

        public int AddExistPlayList(long trackId, long? existPlayList = null)
        {
            Guard.ThrowIfNull(trackId);
            Guard.ThrowIfNull(existPlayList);

            var playList = _unitOfWork.Playlists.IncludeTracks(c => c.PlaylistId == existPlayList && c.UserPlaylists.Any(x => x.UserId == currentUserId));
            var selectedTrack = _unitOfWork.Tracks.IncludePlayLists(a => a.TrackId == trackId);

            if (playList != null && selectedTrack != null)
            {
                selectedTrack.Playlists.Add(playList);
                playList.Tracks.Add(selectedTrack);
            }
            return _unitOfWork.Save();
        }
    }
}
