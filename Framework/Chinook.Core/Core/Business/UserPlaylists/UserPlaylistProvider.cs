using Chinook.Core.Business.Tracks;
using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Core.Business.UserPlaylists
{
    public class UserPlaylistProvider : Repository<UserPlaylist>, IUserPlaylistProvider
    {
        public UserPlaylistProvider(DbContext context) : base(context)
        {
        }
    }
}
