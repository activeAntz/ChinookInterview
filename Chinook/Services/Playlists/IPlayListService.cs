using Chinook.ClientModels;

namespace Chinook.Services
{
    public interface IPlaylistService
    {
        Task<PlaylistDto> GetPlaylistByIdAsync(long id);
        List<PlaylistsDto> GetPlaylists(); 
        Task<List<PlaylistsDto>> GetFilterPlaylistsByTrackIdAsync(long trackId);
        (bool,long) AddPlaylist(string newPlayListName);
    }
}
