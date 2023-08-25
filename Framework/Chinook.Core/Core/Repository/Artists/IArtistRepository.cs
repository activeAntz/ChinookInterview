using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;

namespace Chinook.Core.Repository.Artists
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<List<Artist>> GetArtistsAsync();
    }
}
