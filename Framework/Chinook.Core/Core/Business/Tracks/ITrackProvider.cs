using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using System.Linq.Expressions;

namespace Chinook.Core.Business.Tracks
{
    public interface ITrackProvider : IRepository<Track>
    {
        Track? IncludePlayLists(Expression<Func<Track, bool>> predicate = null);
    }
}
