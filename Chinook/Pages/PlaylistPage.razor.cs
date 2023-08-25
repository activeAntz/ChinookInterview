using global::Microsoft.AspNetCore.Components;
using Chinook.ClientModels;
using Serilog;
using Chinook.Utilities.Helper;


namespace Chinook.Pages
{
    public partial class PlaylistPage
    {
        [Parameter]
        public long PlaylistId { get; set; }

        private PlaylistDto Playlist = new();
        private List<MessageDto> Message = new();

        protected override async Task OnInitializedAsync()
        {
            await InitialData();
        }

        protected override Task OnParametersSetAsync()
        {
            try
            {
                return InitialData();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        private async Task InitialData()
        {
            await InvokeAsync(StateHasChanged);
            Playlist = await playListService.GetPlaylistByIdAsync(PlaylistId);
            Message = globalErrorService.GetAlertInfo();
        }

        private void FavoriteTrack(long trackId)
        {
            try
            {
                var track = Playlist.Tracks.FirstOrDefault(t => t.TrackId == trackId);
                var state = trackService.AddFavoriteTrack(trackId);
                if (state > 0)
                    globalErrorService.SetInfo($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} added to playlist Favorites.");
                else
                    globalErrorService.SetError($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} can not added to playlist Favorites.");

                InvokeAsync(OnInitializedAsync);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }

        }

        private void UnfavoriteTrack(long trackId)
        {
            try
            {
                var track = Playlist.Tracks.FirstOrDefault(t => t.TrackId == trackId);
                var (state, name) = trackService.RemoveTrack(trackId);
                if (state)
                    globalErrorService.SetInfo($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} removed from playlist {name}.");
                else
                    globalErrorService.SetError($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} can not removed from playlist {name}.");

                InvokeAsync(OnInitializedAsync);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        private void RemoveTrack(long trackId)
        {
            try
            {
                var track = Playlist.Tracks.FirstOrDefault(t => t.TrackId == trackId);
                var (state, name) = trackService.RemoveTrack(trackId, PlaylistId);
                if (state)
                    globalErrorService.SetInfo($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} removed from playlist {name}.");
                else
                    globalErrorService.SetError($"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} can not removed from playlist {name}.");

                InvokeAsync(OnInitializedAsync);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        private void CloseInfoMessage()
        {
            globalErrorService.ClearError();
            InvokeAsync(OnInitializedAsync);
        }
    }
}