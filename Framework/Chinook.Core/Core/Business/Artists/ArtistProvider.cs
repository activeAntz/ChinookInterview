using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Core.Business.Artists
{
    public class ArtistProvider : Repository<Artist>, IArtistProvider
    {
        public ArtistProvider(DbContext context) : base(context)
        {
        }
    }
}
