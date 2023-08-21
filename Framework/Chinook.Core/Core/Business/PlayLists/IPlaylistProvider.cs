using Chinook.Core.Infrastructure.Repositories;
using System.Linq.Expressions;
using Chinook.Core.Data.Models;

namespace Chinook.Core.Business.PlayLists
{
    public interface IPlaylistProvider : IRepository<Playlist>
    {
        Playlist? IncludeTracks(Expression<Func<Playlist, bool>> predicate);
        Task<Playlist?> ThenIncludeTracks(Expression<Func<Playlist, bool>> predicate);
        Task<List<Playlist>> IncludeTracksConditionAsync(Expression<Func<Playlist, bool>> predicate);
    }
}
