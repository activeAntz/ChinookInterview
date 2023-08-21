namespace Chinook.Services
{
    public interface IPlayListService
    {
        Task<ClientModels.Playlist> GetPlaylistAsync(long PlaylistId, string CurrentUserId);
        IList<ClientModels.Playlists> GetPlaylistsByUserId(string CurrentUserId); 
        Task<List<ClientModels.Playlists>> GetFilterPlaylistsAsync(long trackId, string CurrentUserId);
        int AddNewPlaylist(string newPlayListName, string CurrentUserId);
    }
}
