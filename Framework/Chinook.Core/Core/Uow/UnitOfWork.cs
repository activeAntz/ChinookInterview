using Chinook.Core.Repository.Artists;
using Chinook.Core.Repository.PlayLists;
using Chinook.Core.Repository.Tracks;
using Chinook.Core.Repository.UserPlaylists;
using Chinook.Core.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Chinook.Core.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ChinookContext _context;

        public UnitOfWork(ChinookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IArtistRepository Artists { get { return new ArtistRepository(this._context); } }
        public IPlaylistRepository Playlists { get { return new PlaylistRepository(this._context); } }
        public ITrackRepository Tracks { get { return new TrackRepository(this._context); } }
        public IUserPlaylistRepository UserPlaylists { get { return new UserPlaylistRepository(this._context); } }

        public int Save() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
