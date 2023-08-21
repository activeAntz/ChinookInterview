namespace Chinook.Services
{
    public interface IPlayListService
    {
        Task<ClientModels.Playlist> GetPlaylistAsync(long PlaylistId);
        IList<ClientModels.Playlists> GetPlaylistsByUserId(); 
        Task<List<ClientModels.Playlists>> GetFilterPlaylistsAsync(long trackId);
        (bool,long) AddNewPlaylist(string newPlayListName);
    }
}
