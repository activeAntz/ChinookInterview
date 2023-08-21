using Chinook.ClientModels;
using Chinook.Core.Data.Models;

namespace Chinook.Services
{
    public interface IArtistService
    {
        Task<List<ArtistsDto>> GetArtistsAsync();
        ArtistDto GetArtist(long artistId);
    }
}
