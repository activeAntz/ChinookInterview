using Chinook.ClientModels;
using Chinook.Core.Helper;
using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Linq;
using System.Net.NetworkInformation;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IList<PlaylistTrack> GetPlaylistTracks(long ArtistId, string CurrentUserId)
        {
            var Artist = _unitOfWork.Artists.Get(a => a.ArtistId == ArtistId);

            return _unitOfWork.Tracks.Where(a => a.Album.ArtistId == ArtistId)
                        .Include(a => a.Album)
                        .Select(t => new PlaylistTrack()
                        {
                            AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                            TrackId = t.TrackId,
                            TrackName = t.Name,
                            IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == CurrentUserId && up.Playlist.Name == FilterType.Favorites)).Any()
                        })
                        .ToList();
        }

        public int AddFavoriteTrack(long trackId, string CurrentUserId)
        {
            var playListCount = _unitOfWork.Playlists.Count();

            var playList = _unitOfWork.Playlists.Get(c => c.Name == FilterType.Favorites && c.UserPlaylists.Any(x => x.UserId == CurrentUserId));
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

                var dataList = new UserPlaylist { UserId = CurrentUserId, Playlist = playList };
                _unitOfWork.UserPlaylists.Add(dataList);
            }

           return _unitOfWork.Save();
        }

        public (bool, string?) RemoveTrack(long trackId, string CurrentUserId, long? PlaylistId = null)
        {
            var playList = _unitOfWork.Playlists.IncludeTracks(c => (PlaylistId.HasValue ? c.PlaylistId == PlaylistId : c.Name == FilterType.Favorites) && c.UserPlaylists.Any(x => x.UserId == CurrentUserId));
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

        public int AddExistPlayList(long trackId, string CurrentUserId, long? existPlayList = null)
        {
            var playList = _unitOfWork.Playlists.IncludeTracks(c => c.PlaylistId == existPlayList && c.UserPlaylists.Any(x => x.UserId == CurrentUserId));
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
