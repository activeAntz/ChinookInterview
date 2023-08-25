using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Core.Repository.PlayLists
{
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(DbContext context) : base(context) { }

        public async Task<List<Playlist>> GetPlaylistsByUserIdAsync(Expression<Func<Playlist, bool>> predicate)
        {
            return await _dbSet.Where(predicate)
                    .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist).ToListAsync();
        }

        public Playlist? IncludeTracks(Expression<Func<Playlist, bool>> predicate)
        {
            return _dbSet.Include(c => c.Tracks).FirstOrDefault(predicate);
        }

        public async Task<List<Playlist>> IncludeTracksWithConditionAsync(Expression<Func<Playlist, bool>> predicate)
        {
            return await _dbSet.Include(c => c.Tracks).Where(predicate).ToListAsync();
        }

        public async Task<Playlist?> ThenIncludeTracks(Expression<Func<Playlist, bool>> predicate)
        {
            return await _dbSet.Include(c => c.Tracks).ThenInclude(c => c.Album).ThenInclude(c => c.Artist)
                .Include(c => c.Tracks).ThenInclude(c => c.Playlists).ThenInclude(c => c.UserPlaylists)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
