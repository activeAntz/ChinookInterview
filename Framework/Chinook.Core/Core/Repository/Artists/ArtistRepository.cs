using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Core.Repository.Artists
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Artist>> GetArtistsAsync()
        {
            return await _dbSet.Include(c => c.Albums).ToListAsync();
        }
    }
}
