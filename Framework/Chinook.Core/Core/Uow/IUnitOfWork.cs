using Chinook.Core.Business.Artists;
using Chinook.Core.Business.PlayLists;
using Chinook.Core.Business.Tracks;
using Chinook.Core.Business.UserPlaylists;

namespace Chinook.Core.Uow
{
    public interface IUnitOfWork
    {
        IArtistProvider Artists { get; }
        IPlaylistProvider Playlists { get; }
        ITrackProvider Tracks { get; }
        IUserPlaylistProvider UserPlaylists { get; }
        int Save();
        //int CommitWithSave();
    }
}
