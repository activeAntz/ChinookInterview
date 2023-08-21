using Chinook.Core.Business.PlayLists;
using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Core.Business.Tracks
{
    public class TrackProvider : Repository<Track>, ITrackProvider
    {
        public TrackProvider(DbContext context) : base(context) { }

        public Track? IncludePlayLists(Expression<Func<Track, bool>> predicate = null)
        {
            return _dbSet.Include(c => c.Playlists).FirstOrDefault(predicate);
        }
    }
}
