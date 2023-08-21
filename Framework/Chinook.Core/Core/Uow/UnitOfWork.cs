using Chinook.Core.Business.Artists;
using Chinook.Core.Business.PlayLists;
using Chinook.Core.Business.Tracks;
using Chinook.Core.Business.UserPlaylists;
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

        public IArtistProvider Artists { get { return new ArtistProvider(this._context); } }
        public IPlaylistProvider Playlists { get { return new PlaylistProvider(this._context); } }
        public ITrackProvider Tracks { get { return new TrackProvider(this._context); } }
        public IUserPlaylistProvider UserPlaylists { get { return new UserPlaylistProvider(this._context); } }

        public int Save() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
