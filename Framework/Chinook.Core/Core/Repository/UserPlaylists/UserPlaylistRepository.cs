using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Core.Repository.UserPlaylists
{
    public class UserPlaylistRepository : Repository<UserPlaylist>, IUserPlaylistRepository
    {
        public UserPlaylistRepository(DbContext context) : base(context)
        {
        }
    }
}
