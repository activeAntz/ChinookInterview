using Chinook.Core.Infrastructure.Repositories;
using System.Linq.Expressions;
using Chinook.Core.Data.Models;

namespace Chinook.Core.Repository.PlayLists
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<List<Playlist>> GetPlaylistsByUserIdAsync(Expression<Func<Playlist, bool>> predicate);
        Playlist? IncludeTracks(Expression<Func<Playlist, bool>> predicate);
        Task<Playlist> ThenIncludeTracks(Expression<Func<Playlist, bool>> predicate);
        Task<List<Playlist>> IncludeTracksWithConditionAsync(Expression<Func<Playlist, bool>> predicate);
    }
}
