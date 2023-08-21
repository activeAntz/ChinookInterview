using Chinook.ClientModels;

namespace Chinook.Services
{
    public interface ITrackService
    {
        IList<PlaylistTrack> GetPlaylistTracks(long ArtistId, string CurrentUserId);
        int AddFavoriteTrack(long trackId, string CurrentUserId);
        (bool, string?) RemoveTrack(long trackId, string CurrentUserId, long? PlaylistId = null);
        int AddExistPlayList(long trackId, string CurrentUserId, long? existPlayList);
    }
}
