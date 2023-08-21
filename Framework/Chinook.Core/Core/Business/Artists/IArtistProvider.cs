using Chinook.Core.Infrastructure.Repositories;
using Chinook.Core.Data.Models;

namespace Chinook.Core.Business.Artists
{
    public interface IArtistProvider : IRepository<Artist>
    {
    }
}
