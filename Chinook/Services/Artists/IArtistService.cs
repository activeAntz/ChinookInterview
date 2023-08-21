using Chinook.Core.Data.Models;

namespace Chinook.Services
{
    public interface IArtistService
    {
        Task<List<Artist>> GetArtistsAsync();
        Artist GetArtist(long artistId);
    }
}
