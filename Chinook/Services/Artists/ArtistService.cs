using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Chinook.Utilities.Validation;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Chinook.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Artist GetArtist(long artistId)
        {
            Guard.ThrowIfNull(artistId);

            return _unitOfWork.Artists.Get(c => c.ArtistId == artistId);
        }

        public async Task<List<Artist>> GetArtistsAsync()
        {
            return await _unitOfWork.Artists.GetArtistsAsync();
        }
    }
}
