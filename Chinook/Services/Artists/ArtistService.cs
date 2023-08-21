using Chinook.Core.Uow;
using Chinook.Core.Data.Models;

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
            return _unitOfWork.Artists.Get(c => c.ArtistId == artistId);
        }

        public IList<Artist> GetArtists()
        {
            return _unitOfWork.Artists.GetAll();
        }
    }
}
