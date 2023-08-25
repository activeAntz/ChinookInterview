using Chinook.Core.Repository.Artists;
using Chinook.Core.Repository.PlayLists;
using Chinook.Core.Repository.Tracks;
using Chinook.Core.Repository.UserPlaylists;

namespace Chinook.Core.Uow
{
    public interface IUnitOfWork
    {
        IArtistRepository Artists { get; }
        IPlaylistRepository Playlists { get; }
        ITrackRepository Tracks { get; }
        IUserPlaylistRepository UserPlaylists { get; }
        int Save();
    }
}
