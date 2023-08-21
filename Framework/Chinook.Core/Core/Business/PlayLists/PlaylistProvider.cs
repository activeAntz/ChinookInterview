using Chinook.Core.Business.Artists;
using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Core.Business.PlayLists
{
    public class PlaylistProvider : Repository<Playlist>, IPlaylistProvider
    {
        public PlaylistProvider(DbContext context) : base(context) { }

        public List<Playlist> GetPlaylistsByUserId(Expression<Func<Playlist, bool>> predicate)
        {
            return _dbSet.Where(predicate)
                    .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist).ToList();
        }

        public Playlist? IncludeTracks(Expression<Func<Playlist, bool>> predicate)
        {
            return _dbSet.Include(c => c.Tracks).FirstOrDefault(predicate);
        }

        public async Task<List<Playlist>> IncludeTracksConditionAsync(Expression<Func<Playlist, bool>> predicate)
        {
            return await _dbSet.Include(c => c.Tracks).Where(predicate).ToListAsync();
        }

        public async Task<Playlist?> ThenIncludeTracks(Expression<Func<Playlist, bool>> predicate)
        {
            return await _dbSet.Include(c => c.Tracks).ThenInclude(c => c.Album).ThenInclude(c => c.Artist).Include(c => c.UserPlaylists).FirstOrDefaultAsync(predicate);
        }
    }
}
