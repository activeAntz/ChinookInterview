using Chinook.ClientModels;

namespace Chinook.Services
{
    public interface ITrackService
    {
        List<PlaylistTrackDto> GetPlaylistTracksByArtistId(long ArtistId);
        int AddFavoriteTrack(long trackId);
        (bool, string) RemoveTrack(long trackId, long? PlaylistId = null);
        int AddExistPlayList(long trackId, long? existPlayList);
    }
}
