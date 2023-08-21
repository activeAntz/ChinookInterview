using Chinook.Core.Data.Models;

namespace Chinook.Services
{
    public interface IArtistService
    {
        IList<Artist> GetArtists();
        Artist GetArtist(long artistId);
    }
}
