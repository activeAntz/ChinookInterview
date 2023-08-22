using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;

namespace Chinook.Core.Business.UserPlaylists
{
    public interface IUserPlaylistProvider : IRepository<UserPlaylist>
    {
    }
}
