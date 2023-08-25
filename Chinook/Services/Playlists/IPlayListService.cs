using Chinook.ClientModels;

namespace Chinook.Services
{
    public interface IPlaylistService
    {
        Task<PlaylistDto> GetPlaylistByIdAsync(long id);
        Task<List<PlaylistsDto>> GetPlaylistsAsync(); 
        Task<List<PlaylistsDto>> GetFilterPlaylistsByTrackIdAsync(long trackId);
        (bool,long) AddPlaylist(string newPlayListName);

        event Action ClientEventCallBack;
    }
}
