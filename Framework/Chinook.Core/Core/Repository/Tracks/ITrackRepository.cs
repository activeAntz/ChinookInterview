using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using System.Linq.Expressions;

namespace Chinook.Core.Repository.Tracks
{
    public interface ITrackRepository : IRepository<Track>
    {
        Track? IncludePlayLists(Expression<Func<Track, bool>> predicate = null);
    }
}
