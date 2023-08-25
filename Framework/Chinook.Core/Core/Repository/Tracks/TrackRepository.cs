using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Core.Repository.Tracks
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(DbContext context) : base(context) { }

        public Track? IncludePlayLists(Expression<Func<Track, bool>> predicate = null)
        {
            return _dbSet.Include(c => c.Playlists).FirstOrDefault(predicate);
        }
    }
}
