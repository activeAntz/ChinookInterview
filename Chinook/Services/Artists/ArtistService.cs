using Chinook.Core.Uow;
using Chinook.Core.Data.Models;
using Chinook.Utilities.Validation;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using AutoMapper;
using Chinook.ClientModels;

namespace Chinook.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ArtistDto GetArtist(long artistId)
        {
            Guard.ThrowIfNull(artistId);

            var artist = _unitOfWork.Artists.Get(c => c.ArtistId == artistId);
            var mapArtist = _mapper.Map<ArtistDto>(artist);

            return mapArtist;
        }

        public async Task<List<ArtistsDto>> GetArtistsAsync()
        {
            var artists = await _unitOfWork.Artists.GetArtistsAsync();
            var mapArtist = _mapper.Map<List<ArtistsDto>>(artists);
            return mapArtist;
        }
    }
}
